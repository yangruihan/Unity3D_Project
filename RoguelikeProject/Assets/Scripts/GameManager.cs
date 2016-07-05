using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public int level = 1;
	// 游戏关卡数

	public int food = 100;
	// 初试食物量

	public AudioClip dieClip;

	public List<Enemy> enemyList = new List<Enemy> ();
	// 敌人列表

	private Text foodText;		// 显示食物的Text
	private Text gameOverText;	// 显示游戏失败的Text
	private Image dayImage;		// 显示当前天数的Image
	private Text dayText;		// 显示当前天数的Text

	private bool sleepStep = true;

	private Player player;
	private MapManager mapManager;

	[HideInInspector]public bool isEnd = false;

	private static GameManager _instance;

	public static GameManager Instance {
		get {
			return _instance;
		}
	}

	void Awake ()
	{
		_instance = this;

		// 加载的时候不销毁本游戏对象
		DontDestroyOnLoad (this.gameObject);

		// 游戏初始化
		InitGame ();
	}

	/**
	 * 游戏初始化
	 */
	private void InitGame ()
	{
		mapManager = GetComponent<MapManager> ();
		// 初始化地图
		mapManager.InitMap ();

		foodText = GameObject.Find ("FoodText").GetComponent<Text> ();
		foodText.text = "Food : " + food;

		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text> ();
		gameOverText.enabled = false;	// 初始化不显示游戏失败

		dayImage = GameObject.Find ("DayImage").GetComponent<Image> ();
		dayText = GameObject.Find ("DayText").GetComponent<Text> ();
		dayText.text = "Day : " + level;
		// 延迟隐藏
		Invoke ("HideDayImage", 1);

		player = GameObject.Find ("Player").GetComponent<Player> ();

		// 还原标记
		isEnd = false;

		// 清空敌人
		enemyList.Clear ();
	}

	/**
	 * 隐藏显示天数的图片
	 */
	private void HideDayImage () {
		dayImage.gameObject.SetActive (false);	
	}

	/**
	 * 增加食物
	 */
	public void AddFood (int food)
	{
		this.food += food;
		ShowFood ("+" + food);
	}

	/**
	 * 减少食物
	 */
	public void decFood (int food)
	{
		this.food -= food;
		ShowFood ("-" + food);

		// 游戏失败
		if (this.food <= 0) {
			// 播放死亡声音
			AudioManager.Instance.RandomPlay (dieClip);
			// 停止背景音乐
			AudioManager.Instance.StopBGAudio ();

			gameOverText.text = "Game Over\n\nDay : " + level; 
			gameOverText.enabled = true;

			// 清空地图对象
			ClearMap ();
		}
	}

	/**
	 * 清空地图对象
	 */
	private void ClearMap ()
	{
		GameObject map = GameObject.Find ("Map");

		for (int i = 0; i < map.transform.childCount; i++) {
			Transform t = map.transform.GetChild (i);
			GameObject.Destroy (t.gameObject);
		}
	}

	/**
	 * 当玩家移动时，调用该方法
	 */
	public void OnPlayerMove ()
	{
		if (sleepStep == true) {
			sleepStep = false;
		} else {
			foreach (var enemy in enemyList) {
				enemy.Move ();
			}
			sleepStep = true;
		}

		// 检测有没有到达终点
		if (player.targetPos == new Vector2(mapManager.cols - 2, mapManager.rows - 2)) {
			isEnd = true;

			// 加载下一关卡
			SceneManager.LoadScene ("Main");	// 重新加载本关卡
		}
	}

	void OnLevelWasLoaded (int level) {
		this.level++;

		// 初始化游戏
		InitGame ();
	}

	/**
	 * 显示食物数量
	 */
	private void ShowFood (string deltaFood)
	{
		if (deltaFood == "0") {
			foodText.text = "Food : " + food;
		} else {
			foodText.text = deltaFood + " Food : " + food;
		}
	}
}

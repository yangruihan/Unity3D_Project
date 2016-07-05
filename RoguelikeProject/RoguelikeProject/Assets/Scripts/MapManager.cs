using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{

	// 外墙对象数组
	public GameObject[] outWallArray;
	// 地板对象数组
	public GameObject[] floorArray;
	// 障碍物对象数组
	public GameObject[] wallArray;
	// 食物对象数组
	public GameObject[] foodArray;
	// 敌人对象数组
	public GameObject[] enemyArray;

	public GameObject exitPrefab;
	// 退出预置体

	public int rows = 10;
	// 地图的行数
	public int cols = 10;
	// 地图的列数

	public int minWall = 2;
	// 最小障碍物数
	public int maxWall = 8;
	// 最大障碍物数

	private Transform mapHolder;
	// 用来放置生成的地图对象

	private GameManager gameManager;
	// 游戏管理类

	/**
	 * 初始化地图
	 */
	public void InitMap ()
	{
		gameManager = GetComponent<GameManager> ();

		mapHolder = new GameObject ("Map").transform;

		// 创建外墙和地板
		InitWallAndFloor ();

		// 创建障碍物、食物、敌人
		InitItems ();

		// 创建退出标记
		InitExit ();
	}

	/**
	 * 创建退出标记
	 */
	private void InitExit ()
	{
		GameObject go = GameObject.Instantiate (exitPrefab, new Vector2 (cols - 2, rows - 2), Quaternion.identity) as GameObject;
		go.transform.SetParent (mapHolder);
	}

	/**
	 * 创建障碍物、食物、敌人
	 */
	private void InitItems ()
	{
		// 需要创建障碍物、食物、敌人的位置
		List<Vector2> positionList = new List <Vector2> ();
		// 初始化位置数组
		for (int x = 2; x < cols - 2; x++) {
			for (int y = 2; y < rows - 2; y++) {
				positionList.Add (new Vector2 (x, y));
			}
		}

		// 创建障碍物
		//  随机生成一个障碍物数量
		int wallNumber = Random.Range (minWall, maxWall);
		RandomGenerateItems (wallNumber, wallArray, positionList);

		// 创建食物
		//  随机生成一个食物数量
		int foodCount = Random.Range (2, Mathf.Min (gameManager.level + 1, positionList.Count));
		RandomGenerateItems (foodCount, foodArray, positionList);

		// 创建敌人
		RandomGenerateItems ((int) Mathf.Min (gameManager.level / 2, positionList.Count), enemyArray, positionList);
	}

	/**
	 * 随机生成物品
	 * count	生成物品的数量
	 * itemArr	生成物品对象的备选数组
	 * positionList	位置数组
	 */
	private void RandomGenerateItems (int count, GameObject[] itemArr, List<Vector2> positionList)
	{
		for (int i = 0; i < count; i++) {
			int index = Random.Range (0, positionList.Count);
			Vector2 position = positionList [index];
			positionList.Remove (position);

			int itemIndex = Random.Range (0, itemArr.Length);
			GameObject go = GameObject.Instantiate (itemArr [itemIndex], position, Quaternion.identity) as GameObject;
			go.transform.SetParent (mapHolder);
		}
	}

	/**
	 * 创建外墙和地板
	 */
	private void InitWallAndFloor ()
	{
		for (int x = 0; x < cols; x++) {
			for (int y = 0; y < rows; y++) {
				// 生成外墙
				if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1) {
					GameObject go = GameObject.Instantiate (outWallArray [Random.Range (0, outWallArray.Length)], new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
					go.transform.SetParent (mapHolder);
				} else { // 生成地板
					GameObject go = GameObject.Instantiate (floorArray [Random.Range (0, floorArray.Length)], new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
					go.transform.SetParent (mapHolder);
				}
			}
		}
	}
}

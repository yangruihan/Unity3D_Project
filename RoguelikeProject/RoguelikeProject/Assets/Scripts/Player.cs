using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	// 移动平滑度
	public float smoothing = 8f;
	// 休息时间
	public float restTime = 0.5f;
	// 休息时间计时器
	private float restTimer = 0;

	public AudioClip[] chopClips;
	public AudioClip[] stepClips;
	public AudioClip[] sodaClips;
	public AudioClip[] foodClips;

	[HideInInspector]public Vector2 targetPos = new Vector2 (1, 1);

	private Rigidbody2D rigidbody;
	private BoxCollider2D collider;
	private Animator animator;

	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		collider = GetComponent<BoxCollider2D> ();
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		rigidbody.MovePosition (Vector2.Lerp (transform.position, targetPos, smoothing * Time.deltaTime));

		// 如果没有食物，则游戏失败，不能进行移动操作
		// 如果到达终点，则进入下一关，也不能进行移动操作
		if (GameManager.Instance.food <= 0 || GameManager.Instance.isEnd == true)
			return;

		restTimer += Time.deltaTime;
		if (restTimer < restTime)
			return;
		else
			restTimer = restTime + 1;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// 水平优先
		if (Mathf.Abs (h) > 0) {
			v = 0;
		}

		if (h != 0 || v != 0) {
			// 每次移动减少食物
			GameManager.Instance.decFood (2);

			// 检测是否撞墙
			collider.enabled = false;
			// 发出射线
			RaycastHit2D hit = Physics2D.Linecast (targetPos, targetPos + new Vector2 (h, v));
			collider.enabled = true;

			// 如果没有碰倒障碍物
			if (hit.transform == null) {
				targetPos += new Vector2 (h, v);
				// 播放脚步声
				AudioManager.Instance.RandomPlay (stepClips);
			} else {
				switch (hit.collider.tag) {
				// 如果碰撞到墙
				case "Wall": 
					// 随机播放音乐
					AudioManager.Instance.RandomPlay (chopClips);
					// 发送消息
					animator.SetTrigger ("Attack");
					hit.transform.SendMessage ("TakeDamage");
					break;

				// 如果碰撞到食物
				case "Food":
					GameManager.Instance.AddFood (10);
					targetPos += new Vector2 (h, v);
					// 播放脚步声
					AudioManager.Instance.RandomPlay (stepClips);
					GameObject.Destroy (hit.transform.gameObject, 0.3f);
					// 播放食物声音
					AudioManager.Instance.RandomPlay (foodClips, 0.3f);
					break;
				
				// 如果碰撞到苏打水
				case "Soda":
					GameManager.Instance.AddFood (20);
					targetPos += new Vector2 (h, v);
					// 播放脚步声
					AudioManager.Instance.RandomPlay (stepClips);
					GameObject.Destroy (hit.transform.gameObject, 0.3f);
					// 播放食物声音
					AudioManager.Instance.RandomPlay (sodaClips, 0.3f);
					break;

				// 如果碰撞敌人
				case "Enemy":
					break;

				// 其他
				default:
					break;
				}
			}

			GameManager.Instance.OnPlayerMove ();
			// 重置计时器
			restTimer = 0;
		}
	}

	public void TakeDamage (int lossFood)
	{
		GameManager.Instance.decFood (lossFood);
		animator.SetTrigger ("Damage");
	}
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float smoothing = 8f;

	public int lossFood = 10;

	public AudioClip[] attackClip;

	private Transform player;
	private Vector2 targetPos;
	private Rigidbody2D rigidbody;
	private BoxCollider2D collider;
	private Animator animator;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		targetPos = transform.position;
		rigidbody = GetComponent<Rigidbody2D> ();
		collider = GetComponent<BoxCollider2D> ();
		animator = GetComponent<Animator> ();

		// 将自身添加到GameManager敌人列表中
		GameManager.Instance.enemyList.Add (this);
	}

	void Update ()
	{
		rigidbody.MovePosition (Vector2.Lerp (transform.position, targetPos, Time.deltaTime * smoothing));
	}

	/**
	 * 移动函数
	 */
	public void Move ()
	{
		Vector2 offset = player.position - transform.position;

		if (offset.magnitude < 1.1f) {
			// 当敌人与主角相距小于1.1时，攻击
			animator.SetTrigger ("Attack");
			//  播放攻击音效
			AudioManager.Instance.RandomPlay (attackClip);
			player.SendMessage ("TakeDamage", lossFood);
		} else { // 当敌人与主角相距大于1.1时，追击
			int x = 0;
			int y = 0;

			// 当x偏移量大于y，则水平移动
			if (Mathf.Abs (offset.x) > Mathf.Abs (offset.y)) {
				if (offset.x > 0) {
					x = 1;
				} else {
					x = -1;
				}
			} else {
				if (offset.y > 0) {
					y = 1;
				} else {
					y = -1;
				}
			}

			// 设置目标位置之前，先检测能否行动
			// 发出射线
			collider.enabled = false;
			RaycastHit2D hit = Physics2D.Linecast (targetPos, targetPos + new Vector2 (x, y));
			collider.enabled = true;

			if (hit.transform == null) {
				targetPos += new Vector2 (x, y);
			} else {
				if (hit.collider.tag == "Food" || hit.collider.tag == "Soda") {
					targetPos += new Vector2 (x, y);
				}
			}
		}
	}
}

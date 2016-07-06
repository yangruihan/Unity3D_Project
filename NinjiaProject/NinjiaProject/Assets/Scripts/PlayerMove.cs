using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

	public float force_move = 120f;
	// 水平移动力
	public float jumpVelocity = 100f;
	// 跳跃速度

	public bool isGround = false;
	// 是否在地面上
	public bool isWall = false;
	// 是否在墙上

	public bool isSlide = false;
	// 是否在滑行

	private Rigidbody2D rigidbody;
	private Animator animator;

	private Transform wallTf;

	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		Vector2 vel = rigidbody.velocity;

		if (isGround) {
			rigidbody.gravityScale = 27;
		}

		if (isSlide == false) {
			rigidbody.gravityScale = 27;
			// 修改方向
			float h = Input.GetAxis ("Horizontal");

			if (h > 0.1f) {
				transform.localScale = new Vector3 (1, 1, 1);
				rigidbody.AddForce (new Vector2 (force_move, 0));
			} else if (h < -0.1f) {
				transform.localScale = new Vector3 (-1, 1, 1);
				rigidbody.AddForce (new Vector2 (-force_move, 0));
			}

			animator.SetFloat ("horizontal", Mathf.Abs (h));
		} else {
			vel.x = 0f;
			rigidbody.velocity = vel; // 将水平速度设置为0
			if (rigidbody.velocity.y < 0.1f) {
				rigidbody.gravityScale = 2;  //10
			}
		}

		// 进行跳跃
		if (Input.GetKeyDown (KeyCode.Space) && (isGround || isSlide)) {
			vel = rigidbody.velocity;
			vel.y = jumpVelocity;

			// 如果在滑行再给一个正方向的速度
			if (isSlide) {
				rigidbody.AddForce (new Vector2 (force_move * transform.localScale.x * 10, 0));

				isSlide = false;
				animator.SetBool ("isSlide", isSlide);

				rigidbody.gravityScale = 27;
			}

			rigidbody.velocity = vel;
		}

		animator.SetFloat ("vertical", vel.y);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Ground") {
			isGround = true;
		}

		if (other.gameObject.tag == "Wall") {
			isWall = true;
			wallTf = other.transform;
		}

		animator.SetBool ("isGround", isGround);
		animator.SetBool ("isWall", isWall);

		if (isWall == true && isGround == false) {
			isSlide = true;
		} else if (isWall == false && isGround == true) {
			isSlide = false;
		}

		animator.SetBool ("isSlide", isSlide);
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Ground") {
			isGround = false;
		}

		if (other.gameObject.tag == "Wall") {
			isWall = false;
		}

		animator.SetBool ("isGround", isGround);
		animator.SetBool ("isWall", isWall);

		if (isWall == true && isGround == false) {
			isSlide = true;
		} else if (isWall == false && isGround == true) {
			isSlide = false;
		}

		animator.SetBool ("isSlide", isSlide);
	}

	/**
	 * 更改主角的朝向
	 */
	public void ChangeDir ()
	{
		if (wallTf.position.x < transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}
}

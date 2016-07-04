using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody2D rigidbody2D;

	public float strength;

	void Start () {
		rigidbody2D = transform.GetComponent<Rigidbody2D> ();
		GoBall ();
	}

	void Update () {
		Vector2 vel = rigidbody2D.velocity;
		if ((vel.x < 9 || vel.x > -9) && vel.x != 0) {
			if (vel.x < 0) {
				vel.x = -10;
			} else {
				vel.x = 10;
			}

			rigidbody2D.velocity = vel;
		}

	}
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.collider.tag.Equals ("Player")) {
			Vector2 vel = rigidbody2D.velocity;
			vel.y = vel.y / 2 + other.rigidbody.velocity.y / 2;
			rigidbody2D.velocity = vel;
		}

		if (other.gameObject.name.Equals ("LeftWall") || other.gameObject.name.Equals ("RightWall")) {
			GameManager.Instance.ChangeScore (other.gameObject.name);
		}
	}

	void Reset () {
		transform.position = Vector3.zero;
		rigidbody2D.velocity = Vector2.zero;
		GoBall ();
	}

	void GoBall () {
		int rand = Random.Range (0, 2);
		if (rand == 0) {
			rigidbody2D.AddForce (new Vector2 (strength, 0));
		} else {
			rigidbody2D.AddForce (new Vector2 (-strength, 0));
		}
	}
}

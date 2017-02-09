using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGravity : MonoBehaviour {

	[SerializeField]
	private float gravityValue;		// 引力大小

	private Rigidbody2D role;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "ship" && (role == null || role.transform != other.transform)) {
			role = other.GetComponent<Rigidbody2D> ();
		}

		var direction = (transform.position - role.transform.position).normalized;
		role.AddForce (direction * gravityValue);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		role = null;
	}
}

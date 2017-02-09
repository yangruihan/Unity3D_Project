using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {

	[SerializeField]
	private float moveForce;		// 移动牵引力

	private Rigidbody2D rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		float x = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
		float y = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		Vector2 forceDir = new Vector2 (x, y).normalized;
		rigidbody.AddForce (forceDir * moveForce);
	}
}

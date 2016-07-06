using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform player;

	void Update () {
		Vector3 tf = transform.position;
		tf.x = player.position.x;
		tf.y = player.position.y;
		transform.position = tf;
	}
}

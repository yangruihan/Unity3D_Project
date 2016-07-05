using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform player1;
	public Transform player2;

	private Vector3 offset;
	private float scaleRatio;
	private Camera camera;

	void Start () {
		offset = transform.position - (player1.position + player2.position) / 2;

		camera = GetComponent<Camera> ();
		scaleRatio = camera.orthographicSize / Vector3.Distance (player1.position, player2.position);
	}
	
	void Update () {
		if (player1 == null || player2 == null) {
			return;
		}
		transform.position = (player1.position + player2.position) / 2 + offset;
		if (Vector3.Distance (player1.position, player2.position) * scaleRatio >= 10) {
			camera.orthographicSize = Vector3.Distance (player1.position, player2.position) * scaleRatio;
		}
	}
}

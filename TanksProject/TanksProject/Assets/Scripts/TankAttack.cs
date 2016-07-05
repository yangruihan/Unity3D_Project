using UnityEngine;
using System.Collections;

public class TankAttack : MonoBehaviour {

	public GameObject shellPrefab;
	public KeyCode fireKey = KeyCode.Space;
	public float shellSpeed = 15;

	public AudioClip shotAudio;

	public Transform firePosition;

	void Start () {
		firePosition = transform.Find ("FirePosition");
	}
	
	void Update () {
		if (Input.GetKeyDown (fireKey)) {
			AudioSource.PlayClipAtPoint (shotAudio, transform.position);
			GameObject go = GameObject.Instantiate (shellPrefab, firePosition.position, firePosition.rotation) as GameObject;
			go.GetComponent<Rigidbody> ().velocity = go.transform.forward * shellSpeed;
		}
	}
}

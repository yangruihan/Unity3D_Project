using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour {

	public float speed = 5;
	public float angularSpeed = 5;

	public int number = 1; //player number

	public AudioClip idleAudio;
	public AudioClip drivingAudio;

	private AudioSource audio;
	private Rigidbody rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
		
	}

	void FixedUpdate (){
		float v = Input.GetAxis ("VerticalPlayer" + number);
		rigidbody.velocity = transform.forward * v * speed;

		float h = Input.GetAxis ("HorizontalPlayer" + number);
		rigidbody.angularVelocity = transform.up * h * angularSpeed;

		if (Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1) {
			audio.clip = drivingAudio;
		} else {
			audio.clip = idleAudio;
		}

		if (audio.isPlaying == false) {
			audio.Play ();
		}
	}
}

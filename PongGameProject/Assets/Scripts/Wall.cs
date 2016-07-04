using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D () {
		audio.pitch = Random.Range (0.8f, 1.2f);
		audio.Play ();
	}
}

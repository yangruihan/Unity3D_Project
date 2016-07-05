using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	public GameObject shellExplosionPrefab;

	public AudioClip shellExplosionAudio;

	void Start () {
		
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		AudioSource.PlayClipAtPoint (shellExplosionAudio, transform.position);
		GameObject.Instantiate (shellExplosionPrefab, transform.position, transform.rotation);
		GameObject.Destroy (this.gameObject);	// Destory itself

		if (other.tag == "Tank") {
			other.SendMessage ("TakeDamage");
		}
	}
}

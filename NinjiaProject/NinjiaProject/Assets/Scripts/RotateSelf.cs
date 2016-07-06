using UnityEngine;
using System.Collections;

public class RotateSelf : MonoBehaviour {

	public float speed = 1000f;

	void Start () {
	
	}
	
	void Update () {
		transform.Rotate (-Vector3.forward * speed * Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class DestoryForTime : MonoBehaviour {

	public float time;

	void Start () {
		Destroy (this.gameObject, time);	
	}
	
	void Update () {
	
	}
}

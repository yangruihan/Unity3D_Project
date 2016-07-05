using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {

	public GameObject tankExplosionPrefab;

	public int maxHp = 100;
	public int hp = 100;

	public AudioClip tankExplosionAudio;

	public Slider hpSlider;

	void Start () {
		
	}
	
	void Update () {
	
	}

	void TakeDamage () {
		if (hp <= 0)
			return;
		
		hp -= Random.Range (10, 20);

		hpSlider.value = (float)hp / maxHp;

		if (hp <= 0) {
			AudioSource.PlayClipAtPoint (tankExplosionAudio, transform.position);
			GameObject.Instantiate (tankExplosionPrefab, transform.position + Vector3.up, transform.rotation);
			GameObject.Destroy (this.gameObject);
		}
	}
}

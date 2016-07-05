using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	public static AudioManager Instance {
		get {
			return _instance;
		}
	}

	void Awake () {
		_instance = this;
	}

	private float minPitch = 0.8f;
	private float maxPitch = 1.2f;

	public AudioSource publicSource;
	public AudioSource bgSource;

	/**
	 * 随机播放音乐
	 */
	public void RandomPlay (params AudioClip[] clips) {
		float pitch = Random.Range (minPitch, maxPitch);
		AudioClip clip = clips[Random.Range(0, clips.Length)];

		publicSource.clip = clip;
		publicSource.pitch = pitch;
		publicSource.Play ();
	}

	/**
	 * 随机延迟播放音乐
	 */
	public void RandomPlay (AudioClip[] clips, float delayed) {
		float pitch = Random.Range (minPitch, maxPitch);
		AudioClip clip = clips[Random.Range(0, clips.Length)];

		publicSource.clip = clip;
		publicSource.pitch = pitch;
		publicSource.PlayDelayed (delayed);
	}

	/**
	 * 停止背景音乐
	 */
	public void StopBGAudio () {
		bgSource.Stop ();
	}
}

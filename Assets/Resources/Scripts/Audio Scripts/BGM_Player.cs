using UnityEngine;
using System.Collections;

public class BGM_Player : MonoBehaviour {


	public AudioClip bgmSource;

	[Tooltip("Volume is on a 0.0 to 1.0 scale")]
	[Range(0.0f,1.0f)]
	public float volume = 1.0f;

	[Range(-3.0f,3.0f)]
	public float pitch = 1.0f;

	public BGM_Player (AudioClip p_bgmSource, float p_volume, float p_pitch = 1.0f) {
		bgmSource = p_bgmSource;
		volume = p_volume;
		pitch = p_pitch;
	}

	void Start () {
		AudioController.stopBGM();
		AudioController.playBGM(bgmSource.name, volume, pitch);
	}

	void Destroy () {
		AudioController.stopBGM();
	}

	void OnDisable () {
		AudioController.stopBGM();
	}
}

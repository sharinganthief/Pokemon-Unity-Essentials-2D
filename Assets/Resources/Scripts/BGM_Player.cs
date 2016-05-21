using UnityEngine;
using System.Collections;

public class BGM_Player : MonoBehaviour {


	public AudioClip bgmSource;

	[Range(0.0f,1.0f)]
	public float volume = 1.0f;

	void Start () {
		AudioController.playBGM(bgmSource.name, volume);
	}

}

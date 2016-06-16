using UnityEngine;
using System.Collections;

public class MetadataSettings : MonoBehaviour {


	public string mapName;

	[Tooltip("If true, a day/night shader will be used on this map")]
	public bool indoorMap = false;

	public AudioClip bgmSource;

	[Tooltip("Volume is on a 0.0 to 1.0 scale")]
	[Range(0.0f,1.0f)]
	public float volume = 1.0f;


	[Range(-3.0f,3.0f)]
	public float pitch = 1.0f;

	// Use this for initialization
	void OnEnable () {
		if (bgmSource != null){
			addBGMPlayer();
		}
	}

	//make new game object and attatch a BGM player, the attach to the parent
	void addBGMPlayer(){
		UnityEngine.GameObject bgmPlayer = new UnityEngine.GameObject("BGM Player");
		bgmPlayer.AddComponent<BGM_Player>();
		bgmPlayer.GetComponent<BGM_Player>().bgmSource = bgmSource;
		bgmPlayer.GetComponent<BGM_Player>().volume = volume;
		bgmPlayer.GetComponent<BGM_Player>().pitch = pitch;
		bgmPlayer.transform.SetParent(gameObject.transform, false);
	}



	void OnDisable(){
		removeBGMPlayer();
	}

	void removeBGMPlayer(){
		foreach (Transform child in transform){
			if (child.gameObject.name.Equals("BGM Player")){
				Destroy(child.gameObject.GetComponent<BGM_Player>());
				Destroy(child.gameObject);
			}
		}
	}


}

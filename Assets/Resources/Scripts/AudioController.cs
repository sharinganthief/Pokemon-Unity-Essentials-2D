using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	//path to SE folder
	public static string audioPath = "Assets/Resources/";
	public static string audioSEPathShort = "Audio/SE/";
	public static string audioBGMPathShort = "Audio/BGM/";
	public static string audioCryPathShort = "Audio/Cries/";

	public static System.IO.DirectoryInfo dirSE = new System.IO.DirectoryInfo(audioPath+audioSEPathShort);
	public static System.IO.DirectoryInfo dirBGM = new System.IO.DirectoryInfo(audioPath+audioBGMPathShort);
	public static System.IO.DirectoryInfo dirCries = new System.IO.DirectoryInfo(audioPath+audioCryPathShort);


	//will contain a list of all desired Audio files
  public static List<System.IO.FileInfo> audioSEFiles = new List<System.IO.FileInfo> ();
	public static List<System.IO.FileInfo> audioBGMFiles = new List<System.IO.FileInfo> ();
	public static List<System.IO.FileInfo> audioCryFiles = new List<System.IO.FileInfo> ();


	//specify all file types to include in the array here.
	private static string[] extensionsAllowed = {"*.wav","*.mp3","*.ogg"};

	private static bool listsCreated = false;

	static void createAudioList(){
		System.IO.FileInfo[] info;
		listsCreated = true;
		foreach (string ext in extensionsAllowed){
			info = dirSE.GetFiles(ext);
			if (info.Length>0){
				foreach (System.IO.FileInfo f in info) {
					audioSEFiles.Add(f);
				}
			}

			info = dirBGM.GetFiles(ext);
			if (info.Length>0){
				foreach (System.IO.FileInfo f in info) {
					audioBGMFiles.Add(f);
				}
			}

			info = dirCries.GetFiles(ext);
			if (info.Length>0){
				foreach (System.IO.FileInfo f in info) {
					audioCryFiles.Add(f);
				}
			}
		}
	}

	static AudioSource audioSource;
	static GameObject tempObject;
	//coroutine to play audio
	//path expected starting in Resources
	//audioSEPathShort, filename
	static IEnumerator playAudio(string path, string filename, float volume = 1.0f, bool loop = false) {
		tempObject = new GameObject ();
		tempObject.AddComponent<AudioSource> ();
		audioSource  = tempObject.GetComponent<AudioSource> ();
		audioSource = tempObject.GetComponent<AudioSource>();
		filename = System.IO.Path.GetFileNameWithoutExtension(filename);
		audioSource.clip = Resources.Load(path+filename, typeof(AudioClip)) as AudioClip;
		audioSource.volume = volume;
		audioSource.loop = loop;
		audioSource.Play();
		yield return null;
	}


	public static void playSE(string filename, float volume = 1.0f){
		if (!listsCreated){
			AudioController.createAudioList();
		}
		if (audioSource!=null && audioSource.isPlaying){
			return;
		} else {
			Destroy(audioSource);
			Destroy(tempObject);
		}
		if (volume<0.0f){
			Debug.Log("Volume must be greater than or equal to 0.0");
			volume = 1.0f;
		} else if (volume>1.0f){
			Debug.Log("Volume must be less than or equal to 1.0");
			volume = 1.0f;
		}
		//check for the exact filename (with specified extension)
		if (audioSEFiles.Exists(x => x.Name==filename)){
			StaticCoroutine.DoCoroutine(AudioController.playAudio(audioSEPathShort,filename,volume));
		}
		//if the filename doesn't have an extension, check each extension
		else if (filename == System.IO.Path.GetFileNameWithoutExtension(filename)) {
			foreach (string ext in extensionsAllowed){
				//Remove function used to remove the * character needed in searching directories
				if (audioSEFiles.Exists(x => x.Name==(filename+(ext.Remove(0, 1))))){
					StaticCoroutine.DoCoroutine(AudioController.playAudio(audioSEPathShort, filename, volume));
					return;
				}
			}
			Debug.Log("Audio file " + filename + " not found.  Ensure the file exists, and is an accepted file typed.");
		}
		else {
			Debug.Log("Audio file " + filename + " not found.  Ensure you typed the correct extension");
		}

	}

	public static void playBGM(string filename, float volume = 1.0f){
		if (!listsCreated){
			AudioController.createAudioList();
		}
		if (audioSource!=null && audioSource.isPlaying){
			return;
		} else {
			Destroy(audioSource);
			Destroy(tempObject);
		}
		if (volume<0.0f){
			Debug.Log("Volume must be greater than or equal to 0.0");
			volume = 1.0f;
		} else if (volume>1.0f){
			Debug.Log("Volume must be less than or equal to 1.0");
			volume = 1.0f;
		}
		//check for the exact filename (with specified extension)
		if (audioBGMFiles.Exists(x => x.Name==filename)){
			StaticCoroutine.DoCoroutine(AudioController.playAudio(audioBGMPathShort, filename, volume, true));
		}
		//if the filename doesn't have an extension, check each extension
		else if (filename == System.IO.Path.GetFileNameWithoutExtension(filename)) {
			foreach (string ext in extensionsAllowed){
				//Remove function used to remove the * character needed in searching directories
				if (audioBGMFiles.Exists(x => x.Name==(filename+(ext.Remove(0, 1))))){
					StaticCoroutine.DoCoroutine(AudioController.playAudio(audioBGMPathShort, filename, volume, true));
					return;
				}
			}
			Debug.Log("Audio file " + filename + " not found.  Ensure the file exists, and is an accepted file typed.");
		}
		else {
			Debug.Log("Audio file " + filename + " not found.  Ensure you typed the correct extension");
		}

	}

}

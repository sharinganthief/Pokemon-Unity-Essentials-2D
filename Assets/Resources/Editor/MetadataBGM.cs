using UnityEngine;
using UnityEditor;

using System.Collections;

public class MetadataBGM : MonoBehaviour {

	[MenuItem("Metadata Tools/Add BGM")]
	static void Create()
	{
		//make new game object and attatch a BGM player
		UnityEngine.GameObject go = new UnityEngine.GameObject("BGM Player");
		go.AddComponent<BGM_Player>();
	}
}

using UnityEngine;
using System.Collections;

public class CheckCurrentMetadata : MonoBehaviour {

	public static MapPositionWatcher mapPositionWatcher;
	public static bool hasFoundPositionWatcher = false;

	public static void findMapPositionWatcher(){
		GameObject[] tempObjects = Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject go in tempObjects){
			if (go.name.Equals("Player")){
				Debug.Log("asfd");
				mapPositionWatcher = go.GetComponent<MapPositionWatcher>();
				hasFoundPositionWatcher = true;
			}
		}
	}

	public static bool isIndoorMap(){
		if (!hasFoundPositionWatcher){
			findMapPositionWatcher();
		}
		return mapPositionWatcher.currentMap().getObjectMap().GetComponent<MetadataSettings>().indoorMap;

	}




}

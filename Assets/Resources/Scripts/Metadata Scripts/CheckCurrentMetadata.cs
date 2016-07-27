using UnityEngine;


public class CheckCurrentMetadata : MonoBehaviour {

	public static MapPositionWatcher mapPositionWatcher;
	public static bool hasFoundPositionWatcher = false;

	public static void findMapPositionWatcher() {
		GameObject[] tempObjects = Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject go in tempObjects) {
			if (go.name.Equals("Player")) {
				mapPositionWatcher = go.GetComponent<MapPositionWatcher>();
				hasFoundPositionWatcher = true;
			}
		}
	}

	public static bool isIndoorMap() {
		if (!hasFoundPositionWatcher) {
			findMapPositionWatcher();
		}
		return mapPositionWatcher.currentMap().getObjectMap().GetComponent<MetadataSettings>().indoorMap;
	}

	public static MapInfo getCurMap() {
		if (!hasFoundPositionWatcher) {
			findMapPositionWatcher();
		}
		return mapPositionWatcher.currentMap();
	}




}

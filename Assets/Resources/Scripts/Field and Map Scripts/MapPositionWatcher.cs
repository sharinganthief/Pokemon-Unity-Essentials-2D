using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//This class loads as a reference to all "maps"
//PlayerMovement should call this each time the player takes a step
//For now, all it does is know when you've made a map change

public class MapPositionWatcher : MonoBehaviour {


	List<MapInfo> loadedMaps = new List<MapInfo> ();
	MapInfo curMap;


	void Awake () {
		 GameObject[] tempObjects = Object.FindObjectsOfType<GameObject>() ;
		 foreach (GameObject go in tempObjects) {
			 if (go.activeInHierarchy && go.GetComponent<Tiled2Unity.TiledMap>() != null) {
				 loadedMaps.Add(new MapInfo(go, go.transform.position.x, go.transform.position.y,
				 				go.GetComponent<Tiled2Unity.TiledMap>().GetMapWidthInPixelsScaled(), go.GetComponent<Tiled2Unity.TiledMap>().GetMapHeightInPixelsScaled()));
			 }
		 }
		 updatePosition();
	}

	// Update is called once per frame
	public void updatePosition() {
		foreach (MapInfo map in loadedMaps) {
			if (gameObject.transform.position.x >= map.getX() && gameObject.transform.position.x < (map.getX() + map.getWidth())) {
				if (gameObject.transform.position.y <= map.getY() && gameObject.transform.position.y > (map.getY() - map.getHeight())) {
					if (map != curMap) {
						if (curMap != null) {
							curMap.getObjectMap().GetComponent<MetadataSettings>().enabled = false;
						}
						curMap = map;
						curMap.getObjectMap().GetComponent<MetadataSettings>().enabled = true;
						//Debug.Log(map.getName());
						break;
					}
				}
			}
		}
	}

	public MapInfo currentMap() {
		return curMap;
	}


}

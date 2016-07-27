using UnityEngine;
using System.Collections;

public class MapInfo {


	GameObject objectMap;
	float x;
	float y;
	float width;
	float height;


	public MapInfo(GameObject p_object, float p_x, float p_y, float p_width, float p_height) {
		objectMap = p_object;
		x = p_x;
		y = p_y;
		width = p_width;
		height = p_height;
	}

	public float getX() {
		return x;
	}

	public float getY() {
		return y;
	}

	public float getWidth() {
		return width;
	}

	public float getHeight() {
		return height;
	}

	public string getName() {
		return objectMap.name;
	}

	public GameObject getObjectMap() {
		return objectMap;
	}

}

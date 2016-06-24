using UnityEngine;
using System.Collections;

public class DayNightSpriteShader : MonoBehaviour {

	private MapInfo curMapName;

	private void Awake()
	{
			// If supported in the sahder set our opacity
			// (Keep opacity at 1.0 to avoid copying the material)
			SpriteRenderer meshRendrer = this.gameObject.GetComponent<SpriteRenderer>();
			curMapName = CheckCurrentMetadata.getCurMap();
			Color dayNightColor = DayNightShading.getCurrentShading();
			meshRendrer.material.SetColor("_Color", dayNightColor);
			InvokeRepeating("updateShading", 0.0f, 60.0f);
	}

	private void updateShading(){
		SpriteRenderer meshRendrer = this.gameObject.GetComponent<SpriteRenderer>();
		Color dayNightColor = DayNightShading.getCurrentShading();
		meshRendrer.material.SetColor("_Color", dayNightColor);
	}

	private void Update(){
		if (curMapName != CheckCurrentMetadata.getCurMap()){
			curMapName = CheckCurrentMetadata.getCurMap();
			updateShading();
		}
	}

}

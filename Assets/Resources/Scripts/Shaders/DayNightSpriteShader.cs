using UnityEngine;
using System.Collections;

public class DayNightSpriteShader : MonoBehaviour {

	private void Awake()
	{
			// If supported in the sahder set our opacity
			// (Keep opacity at 1.0 to avoid copying the material)
			SpriteRenderer meshRendrer = this.gameObject.GetComponent<SpriteRenderer>();
			Color dayNightColor = DayNightShading.getCurrentShading();
			meshRendrer.material.SetColor("_Color", dayNightColor);
	}

}

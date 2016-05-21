using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DayNightShading : MonoBehaviour {

	private Image image;
	private CanvasRenderer canvRend;

	private List<Color> hourlyTones = new List<Color> ();


	public int hour;

	public float baseTone = 0.2f;

	// Use this for initialization
	void OnEnable() {

		//color choices could use some work
		hourlyTones.Add(new Color(0, 0, 1, baseTone));// Midnight
		hourlyTones.Add(new Color(0.1f, 0.1f, 1, baseTone));
		hourlyTones.Add(new Color(0.3f, 0.1f, 1, baseTone));
		hourlyTones.Add(new Color(0.4f, 0.2f, 0.9f, baseTone));
		hourlyTones.Add(new Color(0.6f, 0.3f, 0.7f, baseTone));
		hourlyTones.Add(new Color(0.8f, 0.4f, 0.6f, baseTone));
		hourlyTones.Add(new Color(0.8f, 0.5f, 0.5f, baseTone));// 6AM
		hourlyTones.Add(new Color(0.9f, 0.6f, 0.7f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.7f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.8f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.8f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.9f, 0.9f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.9f, 0.9f, baseTone)); // Noon
		hourlyTones.Add(new Color(0.9f, 0.9f, 0.9f, baseTone));
		hourlyTones.Add(new Color(0.8f, 0.8f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.8f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.9f, 0.7f, 0.7f, baseTone));
		hourlyTones.Add(new Color(0.8f, 0.6f, 0.6f, baseTone));
		hourlyTones.Add(new Color(0.8f, 0.5f, 0.5f, baseTone));// 6PM
		hourlyTones.Add(new Color(0.6f, 0.4f, 0.7f, baseTone));
		hourlyTones.Add(new Color(0.4f, 0.3f, 0.9f, baseTone));
		hourlyTones.Add(new Color(0.3f, 0.2f, 1, baseTone));
		hourlyTones.Add(new Color(0.1f, 0.1f, 1, baseTone));
		hourlyTones.Add(new Color(0, 0, 1, baseTone));

		image = GetComponent<Image>();
		image.rectTransform.sizeDelta = new Vector2(Screen.width,Screen.height);
		hour = TimeFunctions.getHour();
		image.color = hourlyTones[hour];

		//update day/night shading every 60 seconds (possibly change rate, idk)
		InvokeRepeating("updateShading", 1.0f, 60.0f);
	}

	// Update is called once per frame
	void updateShading () {
		hour = TimeFunctions.getHour();
		image.color = hourlyTones[hour];
	}

	//stop the invoke repeating if object is destoryed
	void OnDestroy(){
		CancelInvoke();
	}

	//stop the invoke repeating if object is destoryed
	void OnDisable(){
		CancelInvoke();
	}
}

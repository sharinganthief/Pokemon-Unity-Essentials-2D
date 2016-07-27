using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DayNightShading : MonoBehaviour {


	private static List<Color> hourlyTones = new List<Color> ();


	public static float baseTone = 1f;

	public static bool hasInitializedTones = false;

	// Use this for initialization
	public static void initializeTones() {
		//color choices could use some work
		hourlyTones.Add(new Color(0, 0, 1, baseTone));// Midnight
		hourlyTones.Add(new Color(0.1f, 0.1f, 0.8f, baseTone));
		hourlyTones.Add(new Color(0.5f, 0.5f, 0.75f, baseTone));
		hourlyTones.Add(new Color(0.4f, 0.4f, 0.75f, baseTone));
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
	}

	// Update is called once per frame
	public static Color getCurrentShading () {
		if (!hasInitializedTones) {
			DayNightShading.initializeTones();
		}
		if (CheckCurrentMetadata.isIndoorMap()) {
			return new Color(1,1,1,1);
		}
		int hour = TimeFunctions.getHour();
		return hourlyTones[hour];

	}

}

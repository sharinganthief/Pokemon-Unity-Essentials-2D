using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static GameObject uiManager;

	public static void startManager(){
		uiManager = GameObject.FindGameObjectWithTag("UIManager");
	}

	public static void displayText(string textToDisplay){
		if (uiManager == null){
			startManager();
		}
		if (uiManager.GetComponent<DisplayText>() == null) {
			uiManager.AddComponent<DisplayText>().SetText(textToDisplay);
		}

	}


}

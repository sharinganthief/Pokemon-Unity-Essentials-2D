using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static GameObject uiManager;
	public static GameObject messageSystem;

	public static void startManager(){
		uiManager = GameObject.FindGameObjectWithTag("UIManager");
		messageSystem = GameObject.FindGameObjectWithTag("MessageSystem");
	}

	public static void displayText(string textToDisplay){
		if (uiManager == null || messageSystem == null){
			startManager();
		}
		if (messageSystem.GetComponent<DisplayText>() == null) {
			messageSystem.AddComponent<DisplayText>();
			messageSystem.GetComponent<DisplayText>().SetText(textToDisplay);
		}

	}


}

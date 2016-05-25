using UnityEngine;
using System.Collections;

public class DisplayText : MonoBehaviour {

	float letterPause = 0.1f;
	string words = "";
	private int curWord = 0;
	private string currentSentence = "";

	private bool isTyping = false;
	private bool finishedTyping = false;

	private GUIStyle guiStyle = new GUIStyle();


	void Update(){
		if (Input.GetKey(KeyCode.Space) && !isTyping && !finishedTyping){
			isTyping = true;
			InvokeRepeating("TypeText", 0.0f, letterPause);
		}
		if (finishedTyping && Input.GetButton("Enter")) {
			GUI.Label(new Rect(50, 50, 200, 50), "");
			Destroy(this);
		}
	}

	public void SetText(string newText)
	{
			if (FontManager.hasFont("Power Green")) {
				guiStyle.font = FontManager.getFont("Power Green");
			}
			guiStyle.richText = true;
			guiStyle.fontSize = 20;
			guiStyle.wordWrap = true;
			words = newText;
	}

	void TypeText() {
			string[] tempWords = words.Split(' ');
			currentSentence+= tempWords[curWord];
			curWord++;
			if (curWord>=tempWords.Length){
				isTyping = false;
				curWord = 0;
				finishedTyping = true;
				CancelInvoke("TypeText");
			} else {
				currentSentence += " ";
			}
	}


	void OnGUI() {
			GUI.Label(new Rect(50, 50, 100, 50), currentSentence, guiStyle);
	}
}

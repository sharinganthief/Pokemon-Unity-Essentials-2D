using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

	float letterPause = 0.01f;
	string words = "";
	private int curChar = 0;
	private string currentSentence = "";

	private bool isTyping = false;
	private bool finishedTyping = false;

	private GUIStyle guiStyle = new GUIStyle();


	private Image textBox;
	private Image textBG;
	private Text displayText;
	private GameObject textObj;

	void Start(){

		Image[] tempImages = GetComponentsInChildren<Image>(true);
		foreach (Image image in tempImages){
			if (image.name.Equals("Message Base")){
				textBG = image;
				textBG.enabled = true;
			}
			if (image.name.Equals("Message Skin")){
				textBox = image;
				textBox.enabled = true;
			}
		}

		foreach (Transform child in transform) {
			if (child.name.Equals("Text")){
			 	textObj = child.gameObject;
			}
		}

		if (Screen.width==1024)	{
			textBox.rectTransform.anchoredPosition = new Vector3(-15 , 96 , 0);
			textBG.rectTransform.anchoredPosition = new Vector3(-11 , 96 , 0);
			Debug.Log("object message");
			textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-20, 72, 0);
			textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(945, 96);
		} else if (Screen.width==512) {
			textBox.rectTransform.anchoredPosition = new Vector3(-30 , 152 , 0);
			textBG.rectTransform.anchoredPosition = new Vector3(-26 , 152 , 0);
			textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-45, 120, 0);
			textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(945, 128);
		} else if ((Screen.width/(float)Screen.height).Equals(16/9.0f)){
			textBox.rectTransform.anchoredPosition = new Vector3(-10 , 82 , 0);
			textBG.rectTransform.anchoredPosition = new Vector3(-7 , 82 , 0);
			textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 128, 0);
			textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(1024, 96);
		}
	}

	void Update(){
		if (Input.GetKey(KeyCode.Space) && !isTyping && !finishedTyping){
			isTyping = true;
			InvokeRepeating("TypeText", 0.0f, letterPause);
		}
		if (finishedTyping && Input.GetButton("Enter")) {
			GUI.Label(new Rect(50, 50, 200, 50), "");
			textBox.enabled = false;
			textBG.enabled = false;
			Destroy(this);
		}
	}

	public void SetText(string newText)
	{
			displayText = GetComponentInChildren<Text>();
			if (FontManager.hasFont("Power Green")) {
				displayText.font = FontManager.getFont("Power Green");
			}
			if (Screen.width==1024)	{
				displayText.supportRichText = true;
				displayText.fontSize = 25;
				displayText.horizontalOverflow = HorizontalWrapMode.Wrap;
				displayText.verticalOverflow = VerticalWrapMode.Truncate;
				displayText.lineSpacing = 2;
			} else if (Screen.width==512) {
				displayText.supportRichText = true;
				displayText.fontSize = 35;
				displayText.horizontalOverflow = HorizontalWrapMode.Wrap;
				displayText.verticalOverflow = VerticalWrapMode.Truncate;
				displayText.lineSpacing = 1.75f;
			} else if ((Screen.width/(float)Screen.height).Equals(16/9.0f)){
				displayText.supportRichText = true;
				displayText.fontSize = 42;
				displayText.horizontalOverflow = HorizontalWrapMode.Wrap;
				displayText.verticalOverflow = VerticalWrapMode.Truncate;
				displayText.lineSpacing = 2;
			}
			words = newText;
	}

	void TypeText() {
			char[] tempWords = words.ToCharArray();
			currentSentence+= tempWords[curChar];
			curChar++;
			if (curChar>=tempWords.Length){
				isTyping = false;
				curChar = 0;
				finishedTyping = true;
				CancelInvoke("TypeText");
			}
	}


	void OnGUI() {
		displayText.text = currentSentence;
	}



}

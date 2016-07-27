using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

public class FadeImageInOut : MonoBehaviour {


	private Image image;
	private bool fading = true;

	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<Image>();
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(image.mainTexture.width, image.mainTexture.height);
		InvokeRepeating("startFadeInOut", 0.0f, 0.1f);
	}

	// Update is called once per frame
	public void startFadeInOut () {
		Color c = image.color;
		if (fading) {
    	c.a -= 0.1f;
		} else {
			c.a += 0.05f;
		}
		if (c.a <= 0.0f) {
			fading = false;
			c.a = 0.0f;
		}
		if (c.a >= 1.0f) {
			fading = true;
			c.a = 1.0f;
		}
    image.color = c;
	}

	void Destroy () {
		CancelInvoke("fadeImageInOut");
	}

	void OnDisable () {
		CancelInvoke("fadeImageInOut");
	}

	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			Application.LoadLevel("MainMap");
		}
	}

}

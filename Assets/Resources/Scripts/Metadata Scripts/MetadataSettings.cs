using UnityEngine;
using System.Collections;

public class MetadataSettings : MonoBehaviour {


	public string mapName;

	[Tooltip("If true, a day/night shader will be used on this map")]
	public bool indoorMap = false;

	public AudioClip bgmSource;

	[Tooltip("Volume is on a 0.0 to 1.0 scale")]
	[Range(0.0f,1.0f)]
	public float volume = 1.0f;


	[Range(-3.0f,3.0f)]
	public float pitch = 1.0f;

	// Use this for initialization
	void OnEnable () {
		if (bgmSource != null){
			addBGMPlayer();
		}

		if (!indoorMap){
			addDayNightCycle();
		}
	}

	//make new game object and attatch a BGM player, the attach to the parent
	void addBGMPlayer(){
		UnityEngine.GameObject bgmPlayer = new UnityEngine.GameObject("BGM Player");
		bgmPlayer.AddComponent<BGM_Player>();
		bgmPlayer.GetComponent<BGM_Player>().bgmSource = bgmSource;
		bgmPlayer.GetComponent<BGM_Player>().volume = volume;
		bgmPlayer.GetComponent<BGM_Player>().pitch = pitch;
		bgmPlayer.transform.SetParent(gameObject.transform, false);
	}

	//make new game object and make the appropriate attatchments for the day night cycle, then attach to the parent
	void addDayNightCycle(){
		//make new game object with canvas properties
		UnityEngine.GameObject dnShader = new UnityEngine.GameObject("Day/Night Shader");
		dnShader.AddComponent<UnityEngine.Canvas>();
		UnityEngine.Canvas canv = dnShader.GetComponent<UnityEngine.Canvas>();
		canv.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
		dnShader.AddComponent<UnityEngine.UI.CanvasScaler>();
		dnShader.AddComponent<UnityEngine.CanvasRenderer>();
		dnShader.AddComponent<UnityEngine.UI.GraphicRaycaster>();
		dnShader.layer = UnityEngine.LayerMask.NameToLayer("UI");
		//dnShader.AddComponent<UnityEngine.RectTransform>();
		UnityEngine.UI.CanvasScaler canvScale = dnShader.GetComponent<UnityEngine.UI.CanvasScaler>();
		canvScale.uiScaleMode = (UnityEngine.UI.CanvasScaler.ScaleMode) UnityEngine.ScaleMode.ScaleAndCrop;
		dnShader.transform.SetParent(gameObject.transform, false);

		//add child panel
		UnityEngine.GameObject panel = new UnityEngine.GameObject("Panel");
		panel.AddComponent<UnityEngine.CanvasRenderer>();
		panel.AddComponent<UnityEngine.RectTransform>();
		panel.layer = UnityEngine.LayerMask.NameToLayer("UI");
		panel.transform.SetParent(dnShader.transform, false);


		//add image and shading script
		UnityEngine.GameObject image = new UnityEngine.GameObject("Image");
		image.AddComponent<UnityEngine.CanvasRenderer>();
		image.AddComponent<UnityEngine.UI.Image>();
		image.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(1,1,1,0.0f);
		image.AddComponent<DayNightShading>();
		image.layer = UnityEngine.LayerMask.NameToLayer("UI");
		image.transform.SetParent(panel.transform, false);


		panel.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new UnityEngine.Vector2(0, 0);
		panel.GetComponent<UnityEngine.RectTransform>().anchorMax = new UnityEngine.Vector2(1.0f, 1.0f);
		panel.GetComponent<UnityEngine.RectTransform>().anchorMin = new UnityEngine.Vector2(0, 0);
	}

	void OnDisable(){
		removeBGMPlayer();
		removeDayNightCycle();
	}

	void removeBGMPlayer(){
		foreach (Transform child in transform){
			if (child.gameObject.name.Equals("BGM Player")){
				Destroy(child.gameObject.GetComponent<BGM_Player>());
				Destroy(child.gameObject);
			}
		}
	}

	void removeDayNightCycle(){
		foreach (Transform child in transform){
			if (child.gameObject.name.Equals("Day/Night Shader")){
				foreach (Transform obj2 in child.transform){
					Destroy(obj2.gameObject);
				}
				Destroy(child.gameObject);
			}
		}
	}

}

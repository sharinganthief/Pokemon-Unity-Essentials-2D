//Make sure to place this script in a folder called "Editor"
using UnityEditor;

public class MetadataDayNight
{
    [MenuItem("Metadata Tools/Add Day-Night Shader")]
    static void Create()
    {
				//make new game object with canvas properties
        UnityEngine.GameObject go = new UnityEngine.GameObject("Day/Night Shader");
        go.AddComponent<UnityEngine.Canvas>();
				UnityEngine.Canvas canv = go.GetComponent<UnityEngine.Canvas>();
				canv.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
				go.AddComponent<UnityEngine.UI.CanvasScaler>();
				go.AddComponent<UnityEngine.CanvasRenderer>();
				go.AddComponent<UnityEngine.UI.GraphicRaycaster>();
				go.layer = UnityEngine.LayerMask.NameToLayer("UI");
				go.AddComponent<UnityEngine.RectTransform>();
				UnityEngine.UI.CanvasScaler canvScale = go.GetComponent<UnityEngine.UI.CanvasScaler>();
				canvScale.uiScaleMode = (UnityEngine.UI.CanvasScaler.ScaleMode) UnityEngine.ScaleMode.ScaleAndCrop;

				//add child panel
				UnityEngine.GameObject panel = new UnityEngine.GameObject("Panel");
				panel.AddComponent<UnityEngine.CanvasRenderer>();
				panel.AddComponent<UnityEngine.RectTransform>();
				panel.layer = UnityEngine.LayerMask.NameToLayer("UI");
				panel.transform.parent = go.transform;


				//add image and shading script
				UnityEngine.GameObject image = new UnityEngine.GameObject("Image");
				image.AddComponent<UnityEngine.CanvasRenderer>();
				image.AddComponent<UnityEngine.UI.Image>();
				image.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(1,1,1,0.0f);
				image.AddComponent<DayNightShading>();
				image.layer = UnityEngine.LayerMask.NameToLayer("UI");
				image.transform.parent = panel.transform;


				panel.GetComponent<UnityEngine.RectTransform>().anchoredPosition = new UnityEngine.Vector2(0, 0);
				panel.GetComponent<UnityEngine.RectTransform>().anchorMax = new UnityEngine.Vector2(1.0f, 1.0f);
				panel.GetComponent<UnityEngine.RectTransform>().anchorMin = new UnityEngine.Vector2(0, 0);
    }
}

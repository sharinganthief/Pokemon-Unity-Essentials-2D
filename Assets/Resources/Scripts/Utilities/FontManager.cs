using UnityEngine;
using System.Collections;

public class FontManager : MonoBehaviour {

	//This holds the easy to use font names used by the system ("Power Green, Power Clear, etc")
	public static System.Collections.Generic.List<string> fontNames = new System.Collections.Generic.List<string>();
	//This holds the actual file names
	public static System.Collections.Generic.List<string> fonts = new System.Collections.Generic.List<string>();

	//avoid unnecessary loading if font folder is empty
	public static bool noFontsAvailable = false;


	public static void startFontManager(){
		Object[] tempFonts = (Resources.LoadAll("Fonts", typeof(Font)));
		foreach (Object obj in tempFonts){
			fontNames.Add(((Font)obj).fontNames[0]);
			fonts.Add(obj.name);
		}

		if (fontNames.Count == 0){
			noFontsAvailable = true;
		}
	}

	public static bool hasFont(string fontName){
		if (noFontsAvailable){
			return false;
		}
		if (fontNames.Count == 0){
			startFontManager();
		}
		return fontNames.IndexOf(fontName) != -1;
	}

	public static Font getFont(string fontName){
		return Resources.Load("Fonts/" + fonts[fontNames.IndexOf(fontName)]) as Font;
	}


}

//Make sure to place this script in a folder called "Editor"
using UnityEditor;

public class SceneMetadataInfo
{
    [MenuItem("Metadata Tools/Add Metadata File To Scene")]
    static void Create() {
			//make new game object with canvas properties
			UnityEngine.GameObject go = new UnityEngine.GameObject("Metadata Settings");
			go.AddComponent<MetadataSettings>();
		}
}

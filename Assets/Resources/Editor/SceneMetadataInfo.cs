using UnityEditor;
using UnityEngine;

public class SceneMetadataInfo : MonoBehaviour {

    [MenuItem("GameObject/Metadata/Attach Metadata to prefab", false, 10)]
    static bool AttachMetadata(MenuCommand command) {
      Tiled2Unity.TiledMap tiledMap = ((GameObject)Selection.activeGameObject).GetComponent<Tiled2Unity.TiledMap>();
      if (tiledMap==null){
        return false;
      }

      //attatch the metadata script to the object
			((GameObject)command.context).AddComponent<MetadataSettings>();
      ((GameObject)command.context).GetComponent<MetadataSettings>().enabled = false;
      return true;
		}



}

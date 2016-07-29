using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tiled2Unity
{

  [Tiled2Unity.CustomTiledImporter]
  class CustomTiledImporterTerrainType : Tiled2Unity.ICustomTiledImporter
  {
      public void HandleCustomProperties(UnityEngine.GameObject gameObject,
          IDictionary<string, string> props)
      {
          // Add a terrain type to our game object
          if (props.ContainsKey("terrainType"))
          {
              TerrainType terrainType = (TerrainType) ImportUtils.GetAttributeAsEnum(props["terrainType"], typeof(TerrainType));
              if (terrainType != null) {

                gameObject.AddComponent<TerrainTagChecker>();
                gameObject.GetComponent<TerrainTagChecker>().curTerrainType = terrainType;
                Debug.Log(gameObject.GetComponent<TerrainTagChecker>());
              }
          }

      }

      public void CustomizePrefab(GameObject prefab)
      {
          // Do nothing
      }
  }
}

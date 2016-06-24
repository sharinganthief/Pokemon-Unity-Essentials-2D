using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Tiled2Unity
{
    // Allows us to set shader properties on the Tiled mesh
    // Note: Keep default shader properties imported from Tiled to avoid breaking batching
    // For example, keeping layer opacity to 1.0 (the default) will keep layers using the same material in the same draw call
    public class TiledInitialShaderProperties : MonoBehaviour
    {
        [Range(0, 1)]
        public float InitialOpacity = 1.0f;

        private MapInfo curMapName;
        private void Awake()
        {
            // If supported in the sahder set our opacity
            // (Keep opacity at 1.0 to avoid copying the material)
            MeshRenderer meshRendrer = this.gameObject.GetComponent<MeshRenderer>();
            curMapName = CheckCurrentMetadata.getCurMap();
            Color dayNightColor = DayNightShading.getCurrentShading();
            meshRendrer.material.SetColor("_Color", dayNightColor);
            InvokeRepeating("updateShading", 0.0f, 60.0f);
        }

        private void updateShading(){
          MeshRenderer meshRendrer = this.gameObject.GetComponent<MeshRenderer>();
          Color dayNightColor = DayNightShading.getCurrentShading();
          meshRendrer.material.SetColor("_Color", dayNightColor);
        }

        private void Update(){
          if (curMapName != CheckCurrentMetadata.getCurMap()){
            curMapName = CheckCurrentMetadata.getCurMap();
            updateShading();
          }
        }

    }
}

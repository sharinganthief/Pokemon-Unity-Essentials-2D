using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum TerrainType {
  //don't remove any of these.
  //but feel free to add on your own
  NONE						,
  LEDGE 					,
  GRASS 					,
  SAND	 					,
  ROCK  					,
  DEEPWATER				,
  STILLWATER			,
  WATER						,
  WATERFALL				,
  WATERFALLCREST	,
  TALLGRASS				,
  UNDERWATERGRASS	,
  ICE							,
  SOOTGRASS       ,
  CAVE
}

public class TerrainTagChecker : MonoBehaviour {




  public TerrainType curTerrainType;

  void Start(){
    CompileXMLFiles.compileAbilities();
    CompileXMLFiles.compileTypes();
  }

  public TerrainType getTerrainType(){
    return curTerrainType;
  }


}

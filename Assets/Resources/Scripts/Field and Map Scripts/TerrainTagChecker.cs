using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum TerrainType {
  //don't remove any of these.
  //but feel free to add on your own
  NONE						= 0,
  LEDGE 					= 1,
  GRASS 					= 2,
  SAND	 					= 3,
  ROCK  					= 4,
  DEEPWATER				= 5,
  STILLWATER			= 6,
  WATER						= 7,
  WATERFALL				= 8,
  WATERFALLCREST	= 9,
  TALLGRASS				= 10,
  UNDERWATERGRASS	= 11,
  ICE							= 12,
  SOOTGRASS				= 13
}

public class TerrainTagChecker : MonoBehaviour {




  public TerrainType curTerrainType;

  void Start(){

  }

  public TerrainType getTerrainType(){
    return curTerrainType;
  }


}

using UnityEngine;
using System.Collections;

public class PassabilityCheck : MonoBehaviour {



	public static bool canPass(Rigidbody2D player, Vector2 target, float distance){
		bool ret = true;

		RaycastHit2D hit = Physics2D.Raycast(player.transform.position, target);

		if (hit.collider != null){
			//if you're within a collider
			if (hit.collider.OverlapPoint(player.transform.position + new Vector3(target.x, target.y, player.transform.position.z))){
				if (hit.collider.gameObject.GetComponent<TerrainTagChecker>() != null) {
					TerrainType type = hit.collider.gameObject.GetComponent<TerrainTagChecker>().getTerrainType();
					Debug.Log(type);
				} else if (hit.distance <= distance){
					ret = false;
				}
			//if you would touch a collider
			} else if (hit.distance <= distance){
				if (hit.collider.gameObject.GetComponent<TerrainTagChecker>() != null) {
					Debug.Log(hit.collider.gameObject.GetComponent<TerrainTagChecker>().getTerrainType());
				} else if (hit.distance <= distance){
					ret = false;
				}
			}
		}
		return ret;
	}

}

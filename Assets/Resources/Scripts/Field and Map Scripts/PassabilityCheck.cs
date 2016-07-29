using UnityEngine;
using System.Collections;

public class PassabilityCheck : MonoBehaviour {



	public static bool canPass(Rigidbody2D player, Vector2 target, float distance) {
		bool ret = true;

		BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
		Vector2 playerColliderVector = new Vector2();
		playerColliderVector.Set(  player.transform.position.x+playerCollider.offset.x, player.transform.position.y+playerCollider.offset.y );
		RaycastHit2D hit = Physics2D.Raycast(playerColliderVector, target);

		if (hit.collider != null) {
			if (hit.distance <= distance) {
				if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<TerrainTagChecker>() != null) {
					TerrainType type = hit.collider.gameObject.transform.parent.gameObject.GetComponent<TerrainTagChecker>().getTerrainType();
					Debug.Log(type);
				} else if (hit.distance <= distance) {
					return false;
				}
			}
		} else if (hit.rigidbody != null) {
			Debug.Log(hit.rigidbody.gameObject.name);
		}

		return ret;
	}

}

using UnityEngine;
using System.Collections;

public class PassabilityCheck : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(this);
	}


	public static bool canPass(Rigidbody2D player, Vector2 target, float distance){
		bool ret = true;
		RaycastHit2D hit = Physics2D.Raycast(player.transform.position, target, distance);
		if (hit.collider != null){
			ret = false;
		}
		return ret;
	}

}

using UnityEngine;
using System.Collections;

public class PassabilityCheck : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(this);
	}


	public static bool canPass(Rigidbody2D player, Vector2 target){
		bool ret = true;
		RaycastHit2D hit = Physics2D.Raycast(player.transform.position, target);
		if (hit.collider != null  && hit.distance<=0.32f){
			ret = false;
		}
		Debug.Log(hit.distance);
		return ret;
	}

}

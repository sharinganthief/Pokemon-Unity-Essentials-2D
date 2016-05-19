using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;
	public GameObject player;
	public int setDirection = 0; //0 to retain player direction


	void OnTriggerEnter2D(Collider2D other) {
		  Debug.Log("An object Collided");
		  other.gameObject.transform.position = warpTarget.position;
      Camera.main.transform.position = warpTarget.position + new Vector3(0,0, -10);
			PlayerMovement playerUpdate = player.GetComponent<PlayerMovement> ();
			if (playerUpdate){
				playerUpdate.setTransferred(true, setDirection);
			}
  }
}

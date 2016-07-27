using UnityEngine;
using System.Collections;

public class GamePlayer : MonoBehaviour {

	public static GameObject gamePlayer;

	public static GameObject getPlayer() {
    if (gamePlayer == null) {
			gamePlayer = GameObject.FindGameObjectWithTag("Player");
		}
		return gamePlayer;
  }

	public static float getPlayerX() {
		if (gamePlayer == null) {
			getPlayer();
		}
		return gamePlayer.transform.position.x;
	}

	public static float getPlayerY() {
		if (gamePlayer == null) {
			getPlayer();
		}
		return gamePlayer.transform.position.y;
	}
}

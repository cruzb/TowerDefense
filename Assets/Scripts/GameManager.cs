using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private void Awake() {
		if (instance != null) {
			Debug.Log("Multiple PlayerManagers in scene");
			return;
		}
		instance = this;
	}



	public void EndGame() {

	}

}

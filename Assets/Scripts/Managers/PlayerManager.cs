using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	public static int Money;
	public int startMoney = 200;

	public static int Lives;
	public int startLives = 25;

	private void Awake() {
		if (instance != null) {
			Debug.Log("Multiple PlayerManagers in scene");
			return;
		}
		instance = this;
	}

	private void Start() {
		Money = startMoney;
		Lives = startLives;
	}

	public void RemoveLife() {
		Lives--;
		if(Lives <= 0) {
			GameManager.instance.EndGame();
		}
	}

	//TODO count up to new money + flytext when gained
	public void AddMoney(int money) {
		Money += money;
	}


}

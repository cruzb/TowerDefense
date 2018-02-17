using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	public Text livesText;

	//TODO improve in same way as MoneyUI
	private void Update() {
		livesText.text = "LIVES: " + PlayerManager.Lives;
	}
}

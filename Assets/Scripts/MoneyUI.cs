using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

	public Text moneyText;

	public void SetText(string text) {
		moneyText.text = text;
	}

	//TODO take this out of update for efficiency
	//need a set money call when enemy dies and when money used, etc
	private void Update() {
		moneyText.text = "$" + PlayerManager.Money.ToString();
	}
}

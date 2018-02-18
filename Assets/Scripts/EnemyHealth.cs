using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float health = 100;

	public int moneyValue = 15;

	public GameObject deathEffect;

	public void DoDamage(float damage) {
		health -= damage;

		if(health <= 0) {
			Die();
		}
	}

	private void Die() {
		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		PlayerManager.instance.AddMoney(moneyValue);
		Destroy(gameObject);
	}
}

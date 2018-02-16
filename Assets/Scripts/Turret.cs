using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public Transform target;
	public float range = 10f;

	public string enemyTag = "Enemy";

	public Transform turret;
	public float rotationSpeed = 8f;
	
	private void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	private void Update () {
		if (target == null) return;

		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}


	//gets closest target, maybe change to stay on target until out of range?
	private void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		GameObject nearestEnemy = null;
		float shortestDistance = Mathf.Infinity;

		foreach(GameObject enemy in enemies) {
			float distance = Vector3.Distance(transform.position, enemy.transform.position);

			if(distance < shortestDistance) {
				shortestDistance = distance;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
		}
		else target = null;
	}





	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, range);
	}
}

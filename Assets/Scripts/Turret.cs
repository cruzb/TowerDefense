using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public enum TurretType {
		Projectile,
		Beam
	}
	public TurretType type = TurretType.Projectile;

	[Header("Gameplay Characteristics")]
	public GameObject projectile;

	

	[Header("Projectile (default)")]
	public float fireRate = 1f;
	public float range = 10f;
	private float fireCountdown = 0f;

	[Header("Beam")]
	public LineRenderer beamRenderer;
	public ParticleSystem beamEffect;
	public Light impactLight;
	public int damageOverTime = 35;
	public float slowCoefficient = -1;


	[Header("Functional Data")]
	public Transform target;
	private EnemyHealth targetEnemyHealth; //keep this because getcomponent is expensive 
	private EnemyMover targetEnemyMover;
	public Transform turret;
	private bool firstHitOnTarget = false;
	public float rotationSpeed = 8f;
	public Transform firePosition;

	

	public string enemyTag = "Enemy";

	private void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.4f);
	}
	
	private void Update () {
		if (target == null) {
			if (type == TurretType.Beam) {
				beamRenderer.enabled = false;
				beamEffect.Stop();
				impactLight.enabled = false;

				firstHitOnTarget = false;
			}
			return;
		}

		FindTarget();

		if(type == TurretType.Projectile) {
			if (fireCountdown <= 0f) {
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
		else if(type == TurretType.Beam) {
			DoBeam();
		}
		
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

		Transform lastTarget = target;
		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;

			//if target changes stop affecting previous target
			if (slowCoefficient != -1 && lastTarget != target && lastTarget != null) {
				targetEnemyMover.ResetSpeed();
			}

			targetEnemyHealth = target.GetComponent<EnemyHealth>();
			targetEnemyMover = target.GetComponent<EnemyMover>();
		}
		else {
			if (slowCoefficient != -1 && targetEnemyMover != null)


			target = null;
		}
	}

	private void Shoot() {
		GameObject bulletObj = Instantiate(projectile, firePosition.position, firePosition.rotation);
		Bullet bullet = bulletObj.GetComponent<Bullet>();

		if (bullet != null) {
			bullet.BulletSetup(target);
		}
	}

	private void FindTarget() {
		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (!firstHitOnTarget)
			firstHitOnTarget = !firstHitOnTarget;
	}

	//TODO sometimes fires when not aligned, but still in range so beam doesnt come straight out of barrel
	//add check for direction before firing
	private void DoBeam() {
		//damage
		targetEnemyHealth.DoDamage(damageOverTime * Time.deltaTime);

		if (firstHitOnTarget) {
			if (slowCoefficient != -1) {
				targetEnemyMover.Slow(slowCoefficient);
			}
		}

		if (!beamRenderer.enabled) {
			beamRenderer.enabled = true;
			beamEffect.Play();
			impactLight.enabled = true;
		}
		//do beam rendering
		beamRenderer.SetPosition(0, firePosition.position);
		beamRenderer.SetPosition(1, target.position);

		//do beam impact effect
		Vector3 direction = firePosition.position - target.position;
		beamEffect.transform.rotation = Quaternion.LookRotation(direction);
		beamEffect.transform.position = target.position + direction.normalized;
	}


	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, range);
	}
}

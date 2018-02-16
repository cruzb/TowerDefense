using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public float explosionRadius = 0f;
	public float speed = 70f;

	public GameObject ImpactEffect;

	//for sending data to bullet on init
	//only sends target for now, can send more information for more functionality
	public void BulletSetup(Transform _target) {
		target = _target;
	}

	private void Update () {
		//for edge case when target reaches level end while bullet is in flight
		if(target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 direction = target.position - transform.position;
		float distance = speed * Time.deltaTime;

		//prevent overshooting 
		if(direction.magnitude <= distance) {
			HitTarget();
			return;
		}

		transform.Translate(direction.normalized * distance, Space.World);
		transform.LookAt(target);
	}

	private void HitTarget() {
		GameObject effect = Instantiate(ImpactEffect, transform.position, transform.rotation);
		Destroy(effect, 5f);

		//if this projectile explodes find the things in the explosion radius 
		if(explosionRadius > 0f) {
			Explode();
		}
		//else damage the single target
		else {
			Damage(target);
		}


		Destroy(gameObject);
	}

	private void Explode() {
		Collider[] collidersHit = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in collidersHit) {
			if (collider.tag == "Enemy")
				Damage(collider.transform);
		}
	}

	private void Damage(Transform enemy) {
		Destroy(enemy.gameObject);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {

	public float speed = 15f;

	private Transform target;
	private int waypointIndex = 0;

	private void Start () {
		target = Waypoints.points[waypointIndex];
	}
	
	private void Update () {
		//vector math to get direction to move in
		Vector3 direction = target.position - transform.position;

		///translate towards next waypoint in path at velocity speed
		//direction.normalized - normalize direction so only float speed affects velocity
		//Space.World - move in world space
		transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

		//get next waypoint once we reach the one we are currently moving towards
		if(Vector3.Distance(transform.position, target.position) <= 0.4f) {
			GetNextWaypoint();
		}
	}


	private void GetNextWaypoint() {
		if(waypointIndex >= Waypoints.points.Length - 1) {
			End();
			return;
		}

		waypointIndex++;
		target = Waypoints.points[waypointIndex];
	}

	private void End() {
		PlayerManager.instance.RemoveLife();
		Destroy(gameObject);
	}
}

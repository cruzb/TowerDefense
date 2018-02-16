
using UnityEngine;

public class BuildManager : MonoBehaviour {

	//singleton buildmanager to ensure only one in scene
	//static, available anywhere
	public static BuildManager instance;

	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;
	private GameObject turretToBuild;

	private void Awake() {
		if (instance != null) {
			Debug.Log("Multiple BuildManagers in scene");
			return;
		}
		instance = this;
	}

	public void SetTurretToBuild(GameObject turret) {
		turretToBuild = turret;
	}


	public GameObject GetTurretToBuild() {
		return turretToBuild;
	}
}

using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	public TurretData standardTurretData;
	public TurretData missileLauncherData;
	public TurretData laserTurretData;

	BuildManager buildManager;

	private void Start() {
		buildManager = BuildManager.instance;
	}


	public void SelectStandardTurret() {
		buildManager.SetTurretToBuild(standardTurretData);

	}

	public void SelectMissileLauncher() {
		buildManager.SetTurretToBuild(missileLauncherData);
		
	}

	public void SelectLasertTurret() {
		buildManager.SetTurretToBuild(laserTurretData);
	}
}

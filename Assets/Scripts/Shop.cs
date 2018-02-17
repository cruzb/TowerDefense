using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	public TurretData standardTurretData;
	public TurretData missileLauncherData;

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
}

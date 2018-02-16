using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretData standardTurretData;
	public TurretData missileLauncherData;

	BuildManager buildManager;

	private void Start() {
		buildManager = BuildManager.instance;
	}


	public void PurchaseStandardTurret() {
		buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);

	}

	public void PurchaseMissileLauncher() {
		buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
		
	}
}

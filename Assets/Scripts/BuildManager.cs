
using UnityEngine;

public class BuildManager : MonoBehaviour {

	//singleton buildmanager to ensure only one in scene
	//static, available anywhere
	public static BuildManager instance;

	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;
	private TurretData turretToBuild;

	private void Awake() {
		if (instance != null) {
			Debug.Log("Multiple BuildManagers in scene");
			return;
		}
		instance = this;
	}

	public void SetTurretToBuild(TurretData turret) {
		turretToBuild = turret;
	}

	public void BuildOnNode(Node node) {
		if (PlayerManager.Money < turretToBuild.cost)
			return;

		PlayerManager.Money -= turretToBuild.cost;

		GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;
	}

	public bool CanBuild { get { return turretToBuild != null;  } }
	public bool HasMoney { get { return PlayerManager.Money >= turretToBuild.cost; } }
}

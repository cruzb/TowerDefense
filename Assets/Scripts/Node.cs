using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	BuildManager buildManager;

	private GameObject turret;
	public Vector3 placementOffset;

	//hightlighting
	public Color hoverColor;
	private Color baseColor;
	private Renderer nodeRenderer;

	private void Start() {
		buildManager = BuildManager.instance;
		nodeRenderer = GetComponent<Renderer>();
		baseColor = nodeRenderer.material.color;
	}



	private void OnMouseDown() {
		if (buildManager.GetTurretToBuild() == null)
			return;

		//cant build here as there already is a turret
		if(turret != null) {
			return;
		}

		GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
		turret = Instantiate(turretToBuild, transform.position + placementOffset, transform.rotation);
		buildManager.SetTurretToBuild(null);
	}


	//mouse over highlighting
	private void OnMouseEnter() {
		if (buildManager.GetTurretToBuild() == null)
			return;

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		nodeRenderer.material.color = hoverColor;
	}
	private void OnMouseExit() {
		nodeRenderer.material.color = baseColor;
	}
}

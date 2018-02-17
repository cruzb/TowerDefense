using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	BuildManager buildManager;

	public GameObject turret;
	public Vector3 placementOffset;

	//hightlighting
	//TODO move these to centralized location so each node does not have its own color
	public Color hoverColor;
	public Color failColor;
	private Color baseColor;
	private Renderer nodeRenderer;

	private void Start() {
		buildManager = BuildManager.instance;
		nodeRenderer = GetComponent<Renderer>();
		baseColor = nodeRenderer.material.color;
	}


	public Vector3 GetBuildPosition() {
		return transform.position + placementOffset;
	}


	private void OnMouseDown() {
		if (!buildManager.CanBuild)
			return;

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		//cant build here as there already is a turret
		if(turret != null) {
			return;
		}

		buildManager.BuildOnNode(this);
	}


	//mouse over highlighting
	private void OnMouseEnter() {
		if (!buildManager.CanBuild)
			return;

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (buildManager.HasMoney)
			nodeRenderer.material.color = hoverColor;
		else
			nodeRenderer.material.color = failColor;
	}
	private void OnMouseExit() {
		nodeRenderer.material.color = baseColor;
	}
}

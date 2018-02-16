
using UnityEngine;

public class CameraController : MonoBehaviour {

	private bool allowPan = true;

	public float panSpeed = 25f;
	public float panBorder = 10f; //how far from edge of screen you can pan
	public float scrollSpeed = 4f;
	public float minY = 10f;
	public float maxY = 80f;
	public float minX;

	private void Update () {
		Pan();
		Zoom();
	}

	private void Pan() {
		if (Input.GetKeyDown(KeyCode.Escape))
			allowPan = !allowPan;

		if (!allowPan) return;

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder) {
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBorder) {
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder) {
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBorder) {
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
	}

	private void Zoom() {
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 position = transform.position;
		position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		position.y = Mathf.Clamp(position.y, minY, maxY);
		transform.position = position;
	}
}

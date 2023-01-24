using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[Header("Objects")]
	[SerializeField]
	private Player player;
	[SerializeField]
	private GameController gameController;

	[Header("Camera Movement")]
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float smoothness;
	[SerializeField]
	private float zoomLevel = 3f;
	[SerializeField]
	private Vector2 yClamping = new(-90, 90);

	[Header("Mouse Sensitivity")]
	[SerializeField]
	private float multiplierLeftRight;
	[SerializeField]
	private float multiplierUp;

	private const float minSlowMo = 0.1f;
	private float mouseX;
	private float mouseY;
	private float xRotation;
	private float yRotation;
	private Vector3 currentRotation;
	private bool cameraOrbit = false;
	private Vector3 cameraOffset = Vector3.up;
	private Vector3 timeToSnap = Vector3.zero;

	void Update()
	{
		if (gameController.finish)
			zoomLevel = 5f;

		cameraOrbit = (gameController.isSlowMo && gameController.slowMoTimer - minSlowMo > 0) || gameController.finish || gameController.dead;

		mouseX = Input.GetAxisRaw("Mouse X") * multiplierLeftRight;
		mouseY = Input.GetAxisRaw("Mouse Y") * multiplierUp;

		xRotation += mouseX;
		yRotation -= mouseY;

		yRotation = Mathf.Clamp(yRotation, yClamping.x, yClamping.y);

		if (!cameraOrbit)
		{
			transform.position = player.transform.position + cameraOffset;

			transform.localEulerAngles = Vector3.right * yRotation;
			player.transform.Rotate(Vector3.up * mouseX);

			return;
		}

		Vector3 newRotation = new(yRotation, xRotation);

		currentRotation = Vector3.SmoothDamp(currentRotation, newRotation, ref timeToSnap, smoothness);
		player.gameObject.transform.localEulerAngles = new Vector3(0, currentRotation.y, 0);
		transform.localEulerAngles = new Vector3(currentRotation.x, 0, currentRotation.z);

		transform.position = target.position + Vector3.up * 2 - transform.forward * zoomLevel;
	}
}
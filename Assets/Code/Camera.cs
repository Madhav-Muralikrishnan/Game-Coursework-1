using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
	public Player player;
	public GameController gameController;
	public Transform target;
	public float smoothness;
	public float zoomLevel = 3f;
	public float multiplierLeftRight;
	public float multiplierUp;
	public Vector2 yClamping = new Vector2(-90, 90);
	public float minSlowMo = 0.1f;

	private float mouseX;
	private float mouseY;
	private float xRotation;
	private float yRotation;
	private Vector3 currentRotation;
	private bool cameraOrbit = false;
	private Vector3 cameraOffset = Vector3.up;
	private Vector3 timeToSnap = Vector3.zero;

	private void Update()
	{
		cameraOrbit = gameController.isSlowMo && gameController.slowMoTimer - minSlowMo > 0;

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

		Vector3 newRotation = new Vector3(yRotation, xRotation);

		// Apply damping between rotation changes
		currentRotation = Vector3.SmoothDamp(currentRotation, newRotation, ref timeToSnap, smoothness);
		player.gameObject.transform.localEulerAngles = new Vector3(0, currentRotation.y, 0);
		transform.localEulerAngles = new Vector3(currentRotation.x, 0, currentRotation.z);

		// Substract forward vector of the GameObject to point its forward vector to the target
		transform.position = target.position + Vector3.up * 2 - transform.forward * zoomLevel;
	}

}
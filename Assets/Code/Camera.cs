using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
	public Player player;
	public float multiplierUp = 5f;
	public float multiplierLeftRight = 10f;
	public float cameraVerticalRotation = 0f;

	private float mouseX; 
	private float mouseY;
	private float minRotation = -90f;
	private float maxRotation = 90f;

	private void Update()
	{
		mouseX = Input.GetAxisRaw("Mouse X") * multiplierLeftRight;
		mouseY = Input.GetAxisRaw("Mouse Y") * multiplierUp;

		cameraVerticalRotation -= mouseY;
		cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, minRotation, maxRotation);
		transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
		player.transform.Rotate(Vector3.up * mouseX);
	}
}
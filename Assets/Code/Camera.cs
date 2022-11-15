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
	private bool zoomedOut = false;
	public float zoomOutAmount = 5f;

	void Start()
	{
		//GetComponent<CameraOrbit>.enabled = false;
		
	}

	private void Update()
	{
		mouseX = Input.GetAxisRaw("Mouse X") * multiplierLeftRight;
		mouseY = Input.GetAxisRaw("Mouse Y") * multiplierUp;

		cameraVerticalRotation -= mouseY;
		cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, minRotation, maxRotation);
		transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
		player.transform.Rotate(Vector3.up * mouseX);

		if(Input.GetKey(KeyCode.Mouse0))
		{
			ZoomOut();
		}
		if(Input.GetKeyUp(KeyCode.Mouse0))
		{
			ZoomIn();
		}
	}

	void ZoomOut()
	{
		if(zoomedOut == false)
		{
			for (int i = 0; i < zoomOutAmount; i++ )
			{
				transform.Translate(Vector3.back);
				//GetComponent<CameraOrbit>().enabled = true;
				
				Debug.Log("Zoom");
			}
			//transform.Translate(Vector3.back * zoomOutAmount);
			zoomedOut = true;
		}
	}

	void ZoomIn()
	{
		if(zoomedOut == true)
		{
			transform.Translate(Vector3.forward * zoomOutAmount);
			//GetComponent<CameraOrbit>.enabled = false;
			
			zoomedOut = false;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	private CharacterController controller;
	public Camera followCam;
	public float speed = 0;
	public bool isSlowMo = false;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}


	// Update is called once per frame
	void Update()
	{
		isSlowMo = Input.GetKey(KeyCode.Mouse0);
		Move();
	}

	private void Move()
	{
		float moveZ = Input.GetAxis("Vertical");
		float moveX = Input.GetAxis("Horizontal");

		Vector3 moveInput = Quaternion.Euler(0, followCam.transform.eulerAngles.y,0) * new Vector3(moveX, 0, moveZ);
		Vector3 moveDirection = moveInput.normalized;

		if(moveDirection != Vector3.zero)
		{
			Quaternion desiredRot = Quaternion.LookRotation(moveDirection, Vector3.up);

			transform.rotation = desiredRot;
		}

		controller.Move(moveDirection * speed * Time.deltaTime);
	}

}
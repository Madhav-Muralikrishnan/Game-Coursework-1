using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	private CharacterController controller;
	public Camera followCam;
	public float speed = 0;
	public bool isSlowMo = false;
	public bool canSlowMo = false;
	public double slowMoTimer;
	public double slowMoTimerMax = 2;
	public double regenSlowMoSpeed = 0.5;
	public GameController gameController;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		slowMoTimer = slowMoTimerMax;
	}


	// Update is called once per frame
	void Update()
	{
		if (controller.velocity == Vector3.zero && slowMoTimer < slowMoTimerMax)
		{
			slowMoTimer += Time.deltaTime * regenSlowMoSpeed;
		}

		if (slowMoTimer != 0)
		{
			canSlowMo = true;
			isSlowMo = Input.GetKey(KeyCode.Mouse0);

			if (isSlowMo)
			{
				slowMoTimer -= Time.deltaTime;
				if (slowMoTimer < 0)
				{
					slowMoTimer = 0;
					canSlowMo = false;
				}
			}
		}
		else
		{
			canSlowMo = false;
			isSlowMo = false;
		}

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

	public void Die()
	{
		Debug.Log("You Died!");
		gameController.Respawn();
	}

}

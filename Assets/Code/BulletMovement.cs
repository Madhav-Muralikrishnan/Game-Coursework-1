using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	public float movementSpeed = 5.0f;
	public float slowedSpeed = 0.5f;
	public GameController gameController;
	private Rigidbody rigidBody;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.AddForce(transform.forward * movementSpeed);
	}

	// Update is called once per fixed frame
	void FixedUpdate()
	{
		rigidBody.velocity = Vector3.zero;
		if(gameController.isSlowMo)
		{
			rigidBody.AddForce(transform.forward * slowedSpeed, ForceMode.Impulse);
			return;
		}
		rigidBody.AddForce(transform.forward * movementSpeed, ForceMode.Impulse);
	}

	public void CollideWithWall()
	{
		
	}

	public void CollideWithFloor()
	{

	}
}

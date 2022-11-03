using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovenets2 : MonoBehaviour
{
	public float movementSpeed = 5.0f;
	public float slowedSpeed = 0.5f;
	public ThirdPersonCamera player;
    public Rigidbody rigidBody;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * movementSpeed);
	}

	// Update is called once per fixed frame
	void FixedUpdate()
	{
		if(player.isSlowMo)
		{
			rigidBody.AddForce(transform.forward * -1 * (movementSpeed - slowedSpeed));
			return;
		}
		
		rigidBody.AddForce(transform.forward * movementSpeed);
	}

	public void CollideWithWall()
	{

	}

	public void CollideWithFloor()
	{
		
	}
}

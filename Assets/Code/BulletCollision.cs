using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
	public int numCollisions;
	public int maxCollisions;

	private BulletMovement movement;
	private Player player;

	void Start()
	{
		player = FindObjectOfType<Player>();
		movement = GetComponent<BulletMovement>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			player.Die();
		}

		// if (numCollisions < maxCollisions)
		// {			
		// 	CollideWithLevel(collision);
		// 	return;
		// }
		gameObject.SetActive(false);
	}

	// private void CollideWithLevel(Collision collision)
	// {
	// 	if (collision.gameObject.tag == "Wall")
	// 	{
	// 		movement?.CollideWithWall();
	// 	}
	// 	else if (collision.gameObject.tag == "Floor")
	// 	{
	// 		movement?.CollideWithFloor();
	// 	}
	// 	numCollisions++;
	// }
}

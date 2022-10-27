using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
	public int numCollisions;
	public int maxCollisions;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Debug.LogWarning("Die");
		}
		var movement = GetComponent<BulletMovement>();
		if (numCollisions < maxCollisions)
		{			
			if (collision.gameObject.tag == "Wall")
			{
				movement?.CollideWithWall();
			}
			else if (collision.gameObject.tag == "Floor")
			{
				movement?.CollideWithFloor();
			}
			numCollisions++;
			return;
		}
		gameObject.SetActive(false);
	}
}

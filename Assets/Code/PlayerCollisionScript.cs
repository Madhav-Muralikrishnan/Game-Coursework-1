using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	public GameController gameController;

	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.tag == "Checkpoint")
		{
			Debug.Log("Checkpoint");	
			Vector3 position = collision.gameObject.transform.position;
			Vector3 rotation = new Vector3(0,0,0);

			gameController.SetCheckpoint(position, rotation);
		}
	}
}

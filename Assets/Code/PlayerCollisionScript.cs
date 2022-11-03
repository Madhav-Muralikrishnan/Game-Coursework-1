using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	public GameController gameController;
	private Player player;

	void Start()
	{
		player = GetComponent<Player>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.tag == "Checkpoint")
		{
			Debug.Log("Checkpoint");	
			Vector3 position = collision.gameObject.transform.position;
			Vector3 rotation = new Vector3(0,0,0);

			gameController.SetCheckpoint(position, rotation);
		}
		else if(collision.gameObject.tag == "Finish")
		{
			Debug.Log("Finish");	
			gameController.Finish();
		}
		else if(collision.gameObject.tag == "PowerUp1")
		{
			//Remove some slow mo and destroy bullets in area
			if (gameController.slowMoTimer - 1 > 0)
			{
				gameController.slowMoTimer -= 1;
			}
			else
			{
				gameController.slowMoTimer = 0;
			}
			
			// gameController.destroyInCapsule = true;

			Collider[] colliders = Physics.OverlapSphere(transform.position, 20);

			foreach (Collider collider in colliders)
			{
				Debug.Log(collider?.gameObject?.tag);
				if (collider?.gameObject?.tag == "Bullet")
				{
					Debug.Log("Bullet set false");
					collider.gameObject.SetActive(false);
				}
			}
		}
		else if(collision.gameObject.tag == "PowerUp2")
		{
			Debug.Log("Powerup hit");
			gameController.powerUp2Active = true;
			StartCoroutine(Wait());
		}
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		Debug.Log("Powerup ended");
		gameController.powerUp2Active = false;
	}
}

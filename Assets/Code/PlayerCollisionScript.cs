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
			Debug.Log("Hit PowerUp1");

			//Remove some slow mo and destroy bullets in area
			if (gameController.slowMoTimer - 1 > 0)
			{
				gameController.slowMoTimer -= 1;
			}
			else
			{
				gameController.slowMoTimer = 0;
			}
			
			Collider[] colliders = Physics.OverlapSphere(transform.position, 20);

			foreach (Collider collider in colliders)
			{
				if (collider?.gameObject?.tag == "Bullet")
				{
					collider.gameObject.SetActive(false);
				}
			}
			Destroy(collision.gameObject);
		}
		else if(collision.gameObject.tag == "PowerUp2")
		{
			//Stop slow mo bar decreasing for amount of time
			Debug.Log("Hit PowerUp2");
			Destroy(collision.gameObject);
			gameController.powerUp2Active = true;
			StartCoroutine(Wait());
		}
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		gameController.powerUp2Active = false;
		Debug.Log("PowerUp2 Ended");
	}
}

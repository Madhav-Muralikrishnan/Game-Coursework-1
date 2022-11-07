using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	private GameController gameController;
	private Player player;

	void Start()
	{
		player = GetComponent<Player>();
		gameController = FindObjectOfType<GameController>();
	}

	private void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Checkpoint")
		{
			Checkpoint(collider);
		}
		else if(collider.gameObject.tag == "Finish")
		{
			Debug.Log("Finish");	
			gameController.Finish();
		}
		else if(collider.gameObject.tag == "PowerUp1")
		{
			PowerUp1(collider);
		}
		else if(collider.gameObject.tag == "PowerUp2")
		{
			PowerUp2(collider);
		}
		else if(collider.gameObject.tag == "PowerUp3")
		{
			PowerUp3(collider);
		}
		else if(collider.gameObject.tag == "Key1")
		{
			Debug.Log("Key1 collected");
			Destroy(collider.gameObject);
			gameController.Key1();
		}
		else if(collider.gameObject.tag == "Key2")
		{
			Debug.Log("Key2 collected");
			Destroy(collider.gameObject);
			gameController.Key2();
		}
		else if(collider.gameObject.tag == "LeftClickZone")
		{
			Debug.Log("Left click zone passed");
			gameController.leftClick = true;
		}
		else if(collider.gameObject.tag == "SlowMoZone")
		{
			Debug.Log("Slow mo zone passed");
			gameController.ActivateSlowMoBar();
		}
	}

	private void Checkpoint(Collider collider)
	{
		Debug.Log("Checkpoint");	
		Vector3 position = collider.gameObject.transform.position;
		Vector3 rotation = new Vector3(0,0,0);

		gameController.SetCheckpoint(position, rotation);
	}

	private void PowerUp1(Collider collision)
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

	private void PowerUp2(Collider collider)
	{
		//Stop slow mo bar decreasing for amount of time
		Debug.Log("Hit PowerUp2");
		Destroy(collider.gameObject);
		gameController.powerUp2Active = true;
		gameController.slowMoTimer += 0.1f;
		StartCoroutine(PowerUp2Ending());
	}

	private IEnumerator PowerUp2Ending()
	{
		yield return new WaitForSeconds(3);
		gameController.powerUp2Active = false;
		Debug.Log("PowerUp2 Ended");
	}

	private void PowerUp3(Collider collider)
	{
		//Movement speed doubles
		Debug.Log("Hit PowerUp3");
		Destroy(collider.gameObject);
		player.movementSpeed = player.movementSpeed * 2;
		StartCoroutine(PowerUp3Ending());
	}

	private IEnumerator PowerUp3Ending()
	{
		yield return new WaitForSeconds(3);
		player.movementSpeed = player.movementSpeed / 2;
		Debug.Log("PowerUp3 Ended");
	}
}

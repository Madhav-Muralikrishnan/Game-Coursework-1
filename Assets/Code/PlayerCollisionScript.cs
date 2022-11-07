using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	private GameController gameController;
	private Player player;
	private float powerUp1Cost = 1;
	private float powerUp2Increase = 0.1f;
	private float powerUp3Multiplier = 2;
	private Dictionary<string, Action> tags;

	void Start()
	{
		player = GetComponent<Player>();
		gameController = FindObjectOfType<GameController>();
		tags = new Dictionary<string, Action>();
		tags["Finish"] = () => {Finish();};
		tags["Key1"] = () => {Key1();};
		tags["Key2"] = () => {Key2();};
		tags["LeftClickZone"] = () => {LeftClickZone();};
		tags["SlowMoZone"] = () => {SlowMoZone();};
	}

	private void OnTriggerEnter(Collider collider)
	{
		tags["Checkpoint"] = () => {Checkpoint(collider);};
		tags["PowerUp1"] = () => {PowerUp1(collider);};
		tags["PowerUp2"] = () => {PowerUp2(collider);};
		tags["PowerUp3"] = () => {PowerUp3(collider);};	

		tags[collider.gameObject.tag].Invoke();
	}

	private void Checkpoint(Collider collider)
	{
		Debug.Log("Checkpoint");	
		Vector3 position = collider.gameObject.transform.position;
		Vector3 rotation = new Vector3(0,0,0);

		gameController.SetCheckpoint(position, rotation);
	}

	private void Finish()
	{
		Debug.Log("Finish");	
		gameController.Finish();
	}

	private void PowerUp1(Collider collision)
	{
		Debug.Log("Hit PowerUp1");

		//Remove some slow mo and destroy bullets in area
		if (gameController.slowMoTimer - powerUp1Cost > 0)
		{
			gameController.slowMoTimer -= powerUp1Cost;
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
		gameController.slowMoTimer += powerUp2Increase;
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
		player.movementSpeed = player.movementSpeed * powerUp3Multiplier;
		StartCoroutine(PowerUp3Ending());
	}

	private IEnumerator PowerUp3Ending()
	{
		yield return new WaitForSeconds(3);
		player.movementSpeed = player.movementSpeed / powerUp3Multiplier;
		Debug.Log("PowerUp3 Ended");
	}

	private void Key1()
	{
		Debug.Log("Key1 collected");
		Destroy(GetComponent<Collider>().gameObject);
		gameController.Key1();
	}

	private void Key2()
	{
		Debug.Log("Key2 collected");
		Destroy(GetComponent<Collider>().gameObject);
		gameController.Key2();
	}

	private void LeftClickZone()
	{
		Debug.Log("Left click zone passed");
		gameController.leftClick = true;
	}

	private void SlowMoZone()
	{
		Debug.Log("Slow mo zone passed");
		gameController.ActivateSlowMoBar();
	}
}

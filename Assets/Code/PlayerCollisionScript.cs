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
		tags["Finish"] = () => {gameController.Finish();};
		tags["LeftClickZone"] = () => {gameController.LeftClickZone();};
		tags["SlowMoZone"] = () => {gameController.ActivateSlowMoBar();};
		tags["WASDZone"] = () => {gameController.ActivateZone(2, gameController.wasdInfo);};
		tags["StandStillZone"] = () => {gameController.ActivateZone(2, gameController.standStillInfo);};
		tags["MouseLookZone"] = () => {gameController.ActivateZone(2, gameController.mouseLookInfo);};
		tags["JumpZone"] = () => {gameController.ActivateZone(2, gameController.jumpInfo);};
		tags["P1Zone"] = () => {gameController.ActivateZone(2, gameController.p1Info);};
		tags["P2Zone"] = () => {gameController.ActivateZone(2, gameController.p2Info);};
		tags["P3Zone"] = () => {gameController.ActivateZone(2, gameController.p3Info);};
		tags["DoorZone"] = () => {gameController.ActivateZone(2, gameController.doorInfo);};
		tags["CheckPointZone"] = () => {gameController.ActivateZone(2, gameController.checkPointInfo);};
		tags["HeartBeatZone"] = () => {gameController.heartBeatStart();};
		tags["Enter1"] = () => {gameController.EnterExitRoom(gameController.spawners1, true);};
		tags["Exit1"] = () => {gameController.EnterExitRoom(gameController.spawners1, false);};
		tags["Enter2"] = () => {gameController.EnterExitRoom(gameController.spawners2, true);};
		tags["Exit2"] = () => {gameController.EnterExitRoom(gameController.spawners2, false);};
		tags["Enter3"] = () => {gameController.EnterExitRoom(gameController.room3, true);};
		tags["Exit3"] = () => {gameController.EnterExitRoom(gameController.room3, false);};
		tags["Enter4"] = () => {gameController.EnterExitRoom(gameController.room4, true);};
		tags["Exit4"] = () => {gameController.EnterExitRoom(gameController.room4, false);};
		tags["Enter5"] = () => {gameController.EnterExitRoom(gameController.spawnersFinal, true);};
		tags["Exit5"] = () => {gameController.EnterExitRoom(gameController.spawnersFinal, false);};
	}

	private void OnTriggerEnter(Collider collider)
	{
		tags["Checkpoint"] = () =>
		{
			gameController.SetCheckpoint(collider.gameObject.transform.position, Vector3.zero);
			gameController.checkpointSound.Play();
		};
		
		tags["PowerUp1"] = () => {PowerUp1(collider);};
		tags["PowerUp2"] = () => {PowerUp2(collider);};
		tags["PowerUp3"] = () => {PowerUp3(collider);};	
		tags["Key1"] = () => {Key1(collider);};
		tags["Key2"] = () => {Key2(collider);};
		

		tags[collider.gameObject.tag].Invoke();
	}

	private void PowerUp1(Collider collision)
	{
		Debug.Log("Hit PowerUp1");
		gameController.powerUpSound.Play();

		//Remove some slow mo and destroy bullets in area
		if (gameController.slowMoTimer - powerUp1Cost > 0)
		{
			gameController.slowMoTimer -= powerUp1Cost;
		}
		else
		{
			gameController.slowMoTimer = 0;
		}
		
		Collider[] colliders = Physics.OverlapSphere(transform.position, 100);

		foreach (Collider collider in colliders)
		{
			if (collider?.gameObject?.tag == "BulletSpawner")
			{
				collider.gameObject.GetComponent<BulletSpawner>().EMP();
			}
			else if (collider?.gameObject?.tag == "Bullet")
			{
				collider.gameObject.SetActive(false);
			}
		}
		collision.gameObject.SetActive(false);
		StartCoroutine(RespawnPowerUp(collision.gameObject));
	}

	private void PowerUp2(Collider collider)
	{
		//Stop slow mo bar decreasing for amount of time
		Debug.Log("Hit PowerUp2");
		gameController.powerUpSound.Play();

		collider.gameObject.SetActive(false);
		StartCoroutine(RespawnPowerUp(collider.gameObject));

		gameController.powerUp2Active = true;
		gameController.slowMoTimer += powerUp2Increase;
		StartCoroutine(PowerUp2Ending());
	}

	private IEnumerator PowerUp2Ending()
	{
		yield return new WaitForSeconds(5);
		gameController.powerUp2Active = false;
		Debug.Log("PowerUp2 Ended");
	}

	private void PowerUp3(Collider collider)
	{
		//Movement speed doubles
		Debug.Log("Hit PowerUp3");
		gameController.powerUpSound.Play();

		collider.gameObject.SetActive(false);
		StartCoroutine(RespawnPowerUp(collider.gameObject));

		player.movementSpeed = player.movementSpeed * powerUp3Multiplier;
		StartCoroutine(PowerUp3Ending());
	}

	private IEnumerator PowerUp3Ending()
	{
		yield return new WaitForSeconds(5);
		player.movementSpeed = player.movementSpeed / powerUp3Multiplier;
		Debug.Log("PowerUp3 Ended");
	}

	private void Key1(Collider collider)
	{
		Debug.Log("Key1 collected");
		Destroy(collider.gameObject);
		gameController.Key1();
	}

	private void Key2(Collider collider)
	{
		Debug.Log("Key2 collected");
		Destroy(collider.gameObject);
		gameController.Key2();
	}

	private IEnumerator RespawnPowerUp(GameObject gameObject)
	{
		yield return new WaitForSeconds(5);
		gameObject.SetActive(true);
	} 
}

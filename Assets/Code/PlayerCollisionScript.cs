using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	[SerializeField]
	private GameController gameController;
	[SerializeField]
	private Player player;
	
	private const float powerUp1Cost = 1;
	private const float powerUp2Increase = 0.1f;
	private const float powerUp3Multiplier = 2;

	private Dictionary<string, Action> tags;

	void Start()
	{
        tags = new Dictionary<string, Action>
        {
            ["Finish"] = () => { gameController.Finish(); },
            ["LeftClickZone"] = () => { gameController.LeftClickZone(); },
            ["SlowMoZone"] = () => { gameController.ActivateSlowMoBar(); },
            ["WASDZone"] = () => { gameController.ActivateZone(2, gameController.wasdInfo); },
            ["StandStillZone"] = () => { gameController.ActivateZone(3, gameController.standStillInfo); },
            ["MouseLookZone"] = () => { gameController.ActivateZone(2, gameController.mouseLookInfo); },
            ["JumpZone"] = () => { gameController.ActivateZone(4, gameController.jumpInfo); },
            ["P1Zone"] = () => { gameController.ActivateZone(4, gameController.p1Info); },
            ["P2Zone"] = () => { gameController.ActivateZone(4, gameController.p2Info); },
            ["P3Zone"] = () => { gameController.ActivateZone(4, gameController.p3Info); },
            ["DoorZone"] = () => { gameController.ActivateZone(3, gameController.doorInfo); },
            ["CheckPointZone"] = () => { gameController.ActivateZone(3, gameController.checkPointInfo); },
            ["HeartBeatInfoZone"] = () => { gameController.ActivateZone(6, gameController.heartBeatInfo); },
            ["HeartBeatZone"] = () => { gameController.HeartBeatStart(); },
            ["Enter1"] = () => { gameController.tutorialRoom1Spawners.SetActive(true); },
            ["Exit1"] = () => { gameController.tutorialRoom1Spawners.SetActive(false); },
            ["Enter2"] = () => { gameController.tutorialRoom2Spawners.SetActive(true); },
            ["Exit2"] = () => { gameController.tutorialRoom2Spawners.SetActive(false); },
            ["Enter3"] = () => { gameController.tutorialRooms.SetActive(true); },
            ["Exit3"] = () => { gameController.tutorialRooms.SetActive(false); },
            ["Enter4"] = () => { gameController.startRoom.SetActive(true); },
            ["Exit4"] = () => { gameController.startRoom.SetActive(false); },
            ["Enter5"] = () => { gameController.finalRoomSpawners.SetActive(true); },
            ["Exit5"] = () => { gameController.finalRoomSpawners.SetActive(false); },
            ["DeathFloor"] = () => { player.Die(); }
        };
    }

	private void OnTriggerEnter(Collider collider)
	{
		tags["Checkpoint"] = () =>
		{
			gameController.SetCheckpoint(collider.gameObject.transform.position);
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
			if (collider.gameObject.CompareTag("BulletSpawner"))
			{
				collider.gameObject.GetComponent<BulletSpawner>().EMP();
			}
			else if (collider.gameObject.CompareTag("Bullet"))
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
		gameController.lightningBoltRed.SetActive(true);

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
		gameController.lightningBoltRed.SetActive(false);
		Debug.Log("PowerUp2 Ended");
	}

	private void PowerUp3(Collider collider)
	{
		//Movement speed doubles
		Debug.Log("Hit PowerUp3");
		gameController.powerUpSound.Play();

		collider.gameObject.SetActive(false);
		StartCoroutine(RespawnPowerUp(collider.gameObject));

		player.movementSpeed *= powerUp3Multiplier;
		StartCoroutine(PowerUp3Ending());
	}

	private IEnumerator PowerUp3Ending()
	{
		yield return new WaitForSeconds(5);
		player.movementSpeed /= powerUp3Multiplier;
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

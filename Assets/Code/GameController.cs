using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Player player;
	public GameObject door1;
	public GameObject door2;
	public GameObject slowMoBar;
	public AudioSource heartBeatSound;
	public AudioSource doorSound;
	public Text heartbeatText;
	public float slowMoTimer;
	public float slowMoTimerMax = 2;
	public float regenSlowMoSpeed = 0.5f;
	public bool isSlowMo = false;
	public bool powerUp2Active = false;
	public bool leftClick = false;

	private Vector3 lastCheckPoint;
	private Vector3 lastCheckPointRotation;
	private int numDeaths = 0;
	private float heartBeatsPerSecond = 1;
	private float timerWhenSlowMultiplier = 1;
	private float timer = 0;
	private int totalHeartBeats = 0;
	private int key2sCollected = 0;
	private int key2sNeeded = 4;

	// Start is called before the first frame update
	void Start()
	{
		slowMoTimer = slowMoTimerMax;
		SetCheckpoint(new Vector3(-5,1,-7), new Vector3(0,0,0));
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if (isSlowMo)
			timer += Time.deltaTime * timerWhenSlowMultiplier;

		if (timer >= heartBeatsPerSecond)
		{
			totalHeartBeats++;
			timer = 0;

			//Play heartbeat audio
			heartBeatSound.Play();

			heartbeatText.text = totalHeartBeats.ToString();
		}

	}

	public void Respawn()
	{
		Debug.Log("Respawning to " + lastCheckPoint);
		player.transform.position = lastCheckPoint;
		player.transform.eulerAngles = lastCheckPointRotation;
	}

	public void SetCheckpoint(Vector3 checkPointPosition, Vector3 checkPointRotation)
	{
		lastCheckPoint = checkPointPosition;
		lastCheckPointRotation = checkPointRotation;
	}

	public void AddToDeathCounter()
	{
		numDeaths++;
	}

	public void Finish()
	{
		Debug.Log("Finish");
	}

	public void Key1()
	{
		door1.SetActive(false);
		doorSound.Play();
	}

	public void Key2()
	{
		key2sCollected++;

		if (key2sCollected >= key2sNeeded)
		{
			door2.SetActive(false);
			doorSound.Play();
		}
	}

	public void ActivateSlowMoBar()
	{
		slowMoBar.SetActive(true);
	}
}

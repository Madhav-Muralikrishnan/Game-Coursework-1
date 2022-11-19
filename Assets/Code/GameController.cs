using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Player player;
	public List<GameObject> door1;
	public List<GameObject> door2;
	public List<GameObject> room1;
	public List<GameObject> room2;
	public List<GameObject> room3;
	public List<GameObject> room4;
	public GameObject room5;
	public AudioSource heartBeatSound;
	public float heartBeatVolume = 0.6f;
	public AudioSource doorSound;
	public AudioSource checkpointSound;
	public Text heartbeatText;
	public Text finalTimerText;
	public GameObject finalScreen;
	public CircularProgressBar slowMoBar;
	public GameObject lightningBolt;
	public GameObject slowMoTimerInfo;
	public GameObject leftClickInfo;
	public GameObject mouseLookInfo;
	public GameObject doorInfo;
	public GameObject checkPointInfo;
	public GameObject wasdInfo;
	public GameObject standStillInfo;
	public GameObject jumpInfo;
	public GameObject p1Info;
	public GameObject p2Info;
	public GameObject p3Info;
	public GameObject heartBeatUI;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public float slowMoTimer;
	public float slowMoTimerMax = 2;
	public float regenSlowMoSpeed = 0.5f;
	public bool isSlowMo = false;
	public bool powerUp2Active = false;
	public bool leftClick = false;
	public bool heartbeatStarted = false;
	public bool finish = false;
	public bool dead = false;

	private Vector3 lastCheckPoint;
	private Vector3 lastCheckPointRotation;
	private int numDeaths = 0;
	private float heartBeatsPerSecond = 1;
	private float timerWhenSlowMultiplier = 1;
	private float timer = 0;
	private int totalHeartBeats = 0;
	private int key2sCollected = 0;
	private int key2sNeeded = 4;
	private int twoStars = 100;
	private int threeStars = 50;

	// Start is called before the first frame update
	void Start()
	{
		SetAllFalse(room1);
		SetAllFalse(room2);
		SetAllFalse(room3);
		room5.SetActive(false);
		slowMoTimer = slowMoTimerMax;
		slowMoBar.m_FillColor = Color.blue;
		SetCheckpoint(new Vector3(-63,1,-7), new Vector3(0,0,0));
	}

	// Update is called once per frame
	void Update()
	{
		if (finish)
			return;

		if (heartbeatStarted)
		{
			timer += Time.deltaTime;

			if (isSlowMo)
				timer += Time.deltaTime * timerWhenSlowMultiplier;
		}

		if (timer >= heartBeatsPerSecond)
		{
			totalHeartBeats++;
			timer = 0;

			//Play heartbeat audio
			if (isSlowMo)
			{
				heartBeatSound.volume = 1;
			}
			else
			{
				heartBeatSound.volume = heartBeatVolume;
			}
			heartBeatSound.Play();

			heartbeatText.text = totalHeartBeats.ToString();
		}

		slowMoBar.m_FillAmount = slowMoTimer / slowMoTimerMax;
	}

	public void Respawn()
	{
		Debug.Log("Respawning to " + lastCheckPoint);

		dead = true;
		StartCoroutine(RespawnAfterSeconds(5));
	}

	private IEnumerator RespawnAfterSeconds(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		dead = false;
		slowMoTimer = slowMoTimerMax;
		player.transform.position = lastCheckPoint;
		player.transform.eulerAngles = lastCheckPointRotation;
		SetAllFalse(room1);
		SetAllFalse(room2);
	}

	public void SetCheckpoint(Vector3 checkPointPosition, Vector3 checkPointRotation)
	{
		Debug.Log("Checkpoint");
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
		finish = true;
		finalScreen.SetActive(true);
		heartBeatUI.SetActive(false);
		slowMoBar.gameObject.SetActive(false);
		lightningBolt.SetActive(false);
		finalTimerText.text = totalHeartBeats.ToString();
		
		if (totalHeartBeats < twoStars)
		{
			star1.SetActive(true);
		}
		else if (totalHeartBeats < threeStars)
		{
			star2.SetActive(true);
		}
		else
		{
			star3.SetActive(true);
		}

	}

	public void Key1()
	{
		Movedoors(door1[0], door1[1]);
	}

	public void Key2()
	{
		key2sCollected++;

		if (key2sCollected >= key2sNeeded)
			Movedoors(door2[0], door2[1]);

		room5.SetActive(true);
	}

	private void Movedoors(GameObject left, GameObject right)
	{
		left.transform.position -= new Vector3(3, 0, 0);
		right.transform.position += new Vector3(3, 0, 0);
		doorSound.Play();
	}

	public void ActivateSlowMoBar()
	{
		slowMoBar.gameObject.SetActive(true);
		lightningBolt.SetActive(true);
		ActivateZone(3, slowMoTimerInfo);
	}

	public void LeftClickZone()
	{
		leftClick = true;
		ActivateZone(3, leftClickInfo);
	}

	public void ActivateZone(int seconds, GameObject gameObject)
	{
		gameObject.SetActive(true);
		StartCoroutine(DeleteAfterSeconds(seconds, gameObject));
	}

	private IEnumerator DeleteAfterSeconds(int seconds, GameObject gameObject)
	{
		yield return new WaitForSeconds(seconds);
		gameObject.SetActive(false);
	}

	public void heartBeatStart()
	{
		heartbeatStarted = true;
		heartBeatUI.SetActive(true);
	}

	public void EnterExitRoom(List<GameObject> objects, bool enter)
	{
		foreach(GameObject obj in objects)
		{
			obj.SetActive(enter);
		}
	}

	private void SetAllFalse(List<GameObject> room)
	{
		foreach(GameObject obj in room)
		{
			obj.SetActive(false);
		}
	}
}

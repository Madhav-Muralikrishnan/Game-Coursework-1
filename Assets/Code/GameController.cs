using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Player player;
	public List<GameObject> door1;
	public List<GameObject> door2;
	public AudioSource heartBeatSound;
	public AudioSource doorSound;
	public Text heartbeatText;
	public CircularProgressBar slowMoBar;
	public GameObject lightningBolt;
	public GameObject slowMoTimerInfo;
	public GameObject leftClickInfo;
	public GameObject mouseLookInfo;
	public GameObject wInfo;
	public GameObject adInfo;
	public GameObject jumpInfo;
	public GameObject p1Info;
	public GameObject p2Info;
	public GameObject p3Info;
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

		slowMoBar.m_FillAmount = slowMoTimer / slowMoTimerMax;
	}

	public void Respawn()
	{
		Debug.Log("Respawning to " + lastCheckPoint);
		slowMoTimer = slowMoTimerMax;
		player.transform.position = lastCheckPoint;
		player.transform.eulerAngles = lastCheckPointRotation;
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
}

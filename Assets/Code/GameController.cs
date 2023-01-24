using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[HideInInspector]
	public bool leftClick = false;
	[HideInInspector]
	public bool dead = false;
	[HideInInspector]
	public bool unlimitedSloMo = true;
	[HideInInspector]
	public bool finish = false;
	[HideInInspector]
	public bool isSlowMo = false;
	[HideInInspector]
	public bool powerUp2Active = false;
	[HideInInspector]
	public float slowMoTimer;

	[SerializeField]
	private Player player;

	[Header("Slow Mo")]
	public float slowMoTimerMax = 2;
	public float regenSlowMoSpeed = 0.5f;

	[Header("Tutorial Info")]
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
	public GameObject heartBeatInfo;

	[Header("Rooms")]
	public GameObject tutorialRoom1Spawners;
	public GameObject tutorialRoom2Spawners;
	public GameObject startRoom;
	public GameObject tutorialRooms;
	public GameObject finalRoomSpawners;

	[SerializeField]
	private GameObject finalRoom;
	[SerializeField]
	private List<GameObject> door1;
	[SerializeField]
	private List<GameObject> door2;

	[Header("Audio")]
	public AudioSource checkpointSound;
	public AudioSource powerUpSound;

	[SerializeField]
	private AudioSource music;
	[SerializeField]
	private AudioSource finishMusic;
	[SerializeField]
	private AudioSource doorSound;
	[SerializeField]
	private AudioSource keySound;
	[SerializeField]
	private AudioSource heartBeatSound;
	[SerializeField]
	private float heartBeatVolume = 0.6f;

	[Header("UI")]
	public GameObject lightningBoltRed;

	[SerializeField]
	private Text heartbeatText;
	[SerializeField]
	private CircularProgressBar slowMoBar;
	[SerializeField]
	private GameObject lightningBolt;
	[SerializeField]
	private GameObject heartBeatUI;
	[SerializeField]
	private Text finalTimerText;
	[SerializeField]
	private Text finalDeathsText;
	[SerializeField]
	private GameObject finalScreen;
	[SerializeField]
	private GameObject star1;
	[SerializeField]
	private GameObject star2;
	[SerializeField]
	private GameObject star3;

	private readonly float heartBeatsPerSecond = 1;
	private readonly float timerWhenSlowMultiplier = 1;
	private readonly int key2sNeeded = 4;
	private readonly int twoStars = 100;
	private readonly int threeStars = 50;

	private float timer;
	private int numDeaths;
	private int totalHeartBeats;
	private int key2sCollected;
	private bool heartbeatStarted;
	private Vector3 lastCheckPoint;
	private List<GameObject> uI;

	void Start()
	{
		Cursor.visible = false;

		uI = new List<GameObject>()
		{
			slowMoTimerInfo, leftClickInfo, mouseLookInfo, doorInfo, checkPointInfo,
			wasdInfo, standStillInfo, jumpInfo, p1Info, p2Info, p3Info, heartBeatInfo
		};
		
		slowMoTimer = slowMoTimerMax;
		slowMoBar.m_FillColor = Color.blue;
		SetCheckpoint(new Vector3(-64,1,6));
	}

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
		numDeaths++;
		StartCoroutine(RespawnAfterSeconds(3));
	}

	private IEnumerator RespawnAfterSeconds(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		dead = false;
		slowMoTimer = slowMoTimerMax;
		player.transform.position = lastCheckPoint;

		tutorialRoom1Spawners.SetActive(false);
		tutorialRoom2Spawners.SetActive(false);
		finalRoomSpawners.SetActive(false);
	}

	public void SetCheckpoint(Vector3 checkPointPosition)
	{
		Debug.Log("Checkpoint");
		lastCheckPoint = checkPointPosition;
	}

	public void Finish()
	{
		Debug.Log("Finish");
		finish = true;
		music.Pause();
		finishMusic.Play();
		finalScreen.SetActive(true);
		heartBeatUI.SetActive(false);
		slowMoBar.gameObject.SetActive(false);
		lightningBolt.SetActive(false);
		finalTimerText.text = totalHeartBeats.ToString();
		finalDeathsText.text = numDeaths.ToString();
		
		if (totalHeartBeats > twoStars)
		{
			star1.SetActive(true);
		}
		else if (totalHeartBeats > threeStars)
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
		keySound.Play();
		Movedoors(door1[0], door1[1]);
	}

	public void Key2()
	{
		keySound.Play();
		key2sCollected++;

		if (key2sCollected >= key2sNeeded)
		{
			Movedoors(door2[0], door2[1]);
			finalRoom.SetActive(true);
		}
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
		unlimitedSloMo = false;
		ActivateZone(5, slowMoTimerInfo);
	}

	public void LeftClickZone()
	{
		leftClick = true;
		ActivateZone(3, leftClickInfo);
	}

	public void HeartBeatStart()
	{
		heartbeatStarted = true;
		heartBeatUI.SetActive(true);
	}

	public void ActivateZone(int seconds, GameObject gameObject)
	{
		foreach (GameObject item in uI)
		{
			item.SetActive(item == gameObject);
		}
		StartCoroutine(DeleteAfterSeconds(seconds, gameObject));
	}

	private IEnumerator DeleteAfterSeconds(int seconds, GameObject gameObject)
	{
		yield return new WaitForSeconds(seconds);
		gameObject.SetActive(false);
	}
}

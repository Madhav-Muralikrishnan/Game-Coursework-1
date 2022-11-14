using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public GameObject bullet;
	public int amountToPool;
	public float delayBetweenShoot = 1.0f;
	public float maxRotation;
	public float rotationSpeed;
	public float maxHeight;
	public float verticalSpeed;
	public float maxHorizontal;
	public float horizontalSpeed;
	public bool isTargeting;

	private Player player;
	private List<GameObject> pooledBullets;
	private GameController gameController;
	private float spawnTimer = 0;
	private float timer = 0;
	private float speedRatio;
	private float initialRotation;
	private float initialHeight;
	private float initialHorizontal;
	private bool isWorking = true;


	// Start is called before the first frame update
	void Start()
	{
		BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
		gameController = FindObjectOfType<GameController>();
		player = FindObjectOfType<Player>();

		pooledBullets = new List<GameObject>();
		GameObject temp;
		for(int i = 0; i < amountToPool; i++)
		{
			temp = Instantiate(bullet);
			temp.SetActive(false);
			pooledBullets.Add(temp);
		}

		speedRatio = bulletMovement.slowedSpeed / bulletMovement.movementSpeed;

		initialRotation = transform.eulerAngles.y;
		initialHeight = transform.position.y;
		initialHorizontal = transform.position.x;
	}

	// Update is called once per frame
	void Update()
	{
		if (gameController.isSlowMo)
		{
			spawnTimer += Time.deltaTime * speedRatio;
			timer += Time.deltaTime * speedRatio;
		}
		else
		{
			spawnTimer += Time.deltaTime;
			timer += Time.deltaTime;
		}

		Movement();

		if (spawnTimer >= delayBetweenShoot && isWorking)
			Spawn();
	}

	void Spawn()
	{
		GameObject bullet = GetObject(); 
		if (bullet != null)
		{
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			bullet.SetActive(true);
		}

		spawnTimer = 0;
	}

	GameObject GetObject()
	{
		foreach(GameObject obj in pooledBullets)
		{
			if(!obj.activeInHierarchy)
			{
				return obj;
			}
		}
		return null;
	}

	private void Movement()
	{
		if (isTargeting)
		{
			var rotation = Quaternion.LookRotation(player.gameObject.transform.position - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1);
		}
		else
		{
			float angle = Mathf.Sin(timer * rotationSpeed) * maxRotation;
			angle += initialRotation;
			
			transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
		}

		float horiz = Mathf.Sin(timer * horizontalSpeed) * maxHorizontal;
		horiz += initialHorizontal;

		float height = Mathf.Sin(timer * verticalSpeed) * maxHeight;
		height += initialHeight;

		transform.position = new Vector3(horiz, height, transform.position.z);
	}

	public void EMP()
	{
		isWorking = false;
		StartCoroutine(EmpTimer());
	}

	private IEnumerator EmpTimer()
	{
		yield return new WaitForSeconds(3);
		isWorking = true;
		Debug.Log("EMP Ended");
	}
}

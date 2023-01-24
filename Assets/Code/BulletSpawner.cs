using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	[Header("Shooting")]
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private float delayBetweenShoot = 1.0f;
	[SerializeField]
	private bool isWorking = true;

	[Header("Rotation")]
	[SerializeField]
	private float maxRotation;
	[SerializeField]
	private float rotationSpeed;

	[Header("Movement")]
	[SerializeField]
	private float maxHeight;
	[SerializeField]
	private float verticalSpeed;
	[SerializeField]
	private float maxHorizontal;
	[SerializeField]
	private float horizontalSpeed;

	[Header("Targeting")]
	[SerializeField]
	private bool isTargeting;
	[SerializeField]
	private GameObject target;
	[SerializeField]
	private Vector3 adjustment;

	private readonly List<GameObject> pooledBullets = new();

	private GameController gameController;
	private float spawnTimer;
	private float timer;
	private float speedRatio;
	private float initialRotation;
	private float initialHeight;
	private float initialHorizontal;


	void Start()
	{
		BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
		gameController = FindObjectOfType<GameController>();

		speedRatio = bulletMovement.slowedSpeed / bulletMovement.movementSpeed;

		initialRotation = transform.eulerAngles.y;
		initialHeight = transform.position.y;
		initialHorizontal = transform.position.x;
	}

	void Update()
	{
		if (gameController.finish)
		{
			Destroy(this);
			return;
		}

		if (target == null && isTargeting)
			return;

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

	private void Spawn()
	{
		GameObject bullet = GetObject(); 
		if (bullet != null)
		{
            bullet.transform.SetPositionAndRotation(transform.position + transform.forward, transform.rotation);
			bullet.SetActive(true);
		}

		spawnTimer = 0;
	}

	private GameObject GetObject()
	{
		foreach (GameObject obj in pooledBullets)
		{
			if (!obj.activeInHierarchy)
			{
				return obj;
			}
		}

		var newBullet = Instantiate(bullet);
		newBullet.SetActive(false);
		pooledBullets.Add(newBullet);
		return newBullet;
	}

	private void Movement()
	{
		if (isTargeting)
		{
			var rotation = Quaternion.LookRotation(target.transform.position - transform.position + adjustment);
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

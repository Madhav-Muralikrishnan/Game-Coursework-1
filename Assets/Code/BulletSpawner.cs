using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public float delayBetweenShoot = 1.0f;
	public List<GameObject> pooledBullets;
	public GameObject bullet;
	public int amountToPool;
	public float maxRotation;
	public float rotationSpeed;
	private BulletMovement bulletMovement;
	private GameController gameController;
	private float timer = 0;
	private float speedRatio;
	private float initialRotation;


	// Start is called before the first frame update
	void Start()
	{
		bulletMovement = bullet.GetComponent<BulletMovement>();
		gameController = FindObjectOfType<GameController>();

		timer = 0;

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
	}

	// Update is called once per frame
	void Update()
	{
		float angle = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
		angle += initialRotation;

		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);

		if (gameController.isSlowMo)
		{
			timer += Time.deltaTime * speedRatio;
		}
		else
		{
			timer += Time.deltaTime;
		}

		if (timer >= delayBetweenShoot)
		{
			Spawn();
		}
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

		timer = 0;
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
}

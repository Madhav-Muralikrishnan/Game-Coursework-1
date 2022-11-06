using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public float delayBetweenShoot = 1.0f;
	public GameObject bullet;
	public int amountToPool;
	public float maxRotation;
	public float rotationSpeed;
	public float maxHeight;
	public float verticalSpeed;
	private List<GameObject> pooledBullets;
	private BulletMovement bulletMovement;
	private GameController gameController;
	private float timer = 0;
	private float speedRatio;
	private float initialRotation;
	private float initialHeight;


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
		initialHeight = transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		float angle = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
		angle += initialRotation;

		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);

		float height = Mathf.Sin(Time.time * verticalSpeed) * maxHeight;
		height += initialHeight;

		transform.position = new Vector3(transform.position.x, height, transform.position.z);

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

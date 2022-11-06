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
	public float maxHorizontal;
	public float horizontalSpeed;
	private List<GameObject> pooledBullets;
	private BulletMovement bulletMovement;
	private GameController gameController;
	private float timer = 0;
	private float speedRatio;
	private float initialRotation;
	private float initialHeight;
	private float initialHorizontal;


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
		initialHorizontal = transform.position.x;
	}

	// Update is called once per frame
	void Update()
	{
		Movement();

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

	private void Movement()
	{
		float angle = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
		angle += initialRotation;

		float height = Mathf.Sin(Time.time * verticalSpeed) * maxHeight;
		height += initialHeight;

		float horiz = Mathf.Sin(Time.time * horizontalSpeed) * maxHorizontal;
		horiz += initialHorizontal;

		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
		transform.position = new Vector3(horiz, height, transform.position.z);
	}
}

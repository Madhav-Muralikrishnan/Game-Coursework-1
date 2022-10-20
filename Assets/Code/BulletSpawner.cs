using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float delayBetweenShoot = 1.0f;
    public GameObject bullet;
    private float timer = 0;
    private Vector3 location;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        location = transform.position;

        timer += Time.deltaTime;

        if (timer >= delayBetweenShoot)
        {
            Spawn();
        }
    }

    void Spawn()
    {

        GameObject newBullet = Instantiate(bullet, location, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        timer = 0;
    }
}

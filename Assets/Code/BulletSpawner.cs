using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float delayBetweenShoot = 1.0f;
    public GameObject bullet;
    public BulletMovement bulletMovement;
    public PlayerController player;
    private float timer = 0;
    private float speedRatio;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        speedRatio = bulletMovement.slowedSpeed / bulletMovement.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isSlowMo)
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
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        timer = 0;
    }
}

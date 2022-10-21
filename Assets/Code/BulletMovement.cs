using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float slowedSpeed = 0.5f;
    public ThirdPersonCamera player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        if(player.isSlowMo)
        {
            transform.position += transform.forward * Time.deltaTime * slowedSpeed;
            return;
        }
        
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }
}

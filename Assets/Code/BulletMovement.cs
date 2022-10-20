using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float movementSpeed = 5.0f;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        if(!player.isSlowMo)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }
}

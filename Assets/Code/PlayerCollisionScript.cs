using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Die");
            //Die();
        }
   }
}

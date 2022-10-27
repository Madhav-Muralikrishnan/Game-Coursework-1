using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player player;
    public float slowMoTimer;
    public float slowMoTimerMax = 2;
    public float regenSlowMoSpeed = 0.5f;
    public bool isSlowMo = false;
    private Vector3 lastCheckPoint;
    private Vector3 lastCheckPointRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        slowMoTimer = slowMoTimerMax;
        SetCheckpoint(new Vector3(0,1,0), new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        Debug.Log("Respawning to " + lastCheckPoint);
        player.transform.position = lastCheckPoint;
        player.transform.eulerAngles = lastCheckPointRotation;
    }

    public void SetCheckpoint(Vector3 checkPointPosition, Vector3 checkPointRotation)
    {
        lastCheckPoint = checkPointPosition;
        lastCheckPointRotation = checkPointRotation;
    }
}

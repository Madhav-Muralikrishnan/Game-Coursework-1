using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    public Player player;
    public float multiplierUp = 5f;
    public float multiplierLeftRight = 10f;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        player.transform.Rotate(new Vector3(0f, mouseX * multiplierLeftRight, 0f));
        transform.Rotate(new Vector3(-Mathf.Clamp(mouseY * multiplierUp, -90f, 90f), 0f, 0f));
    }

}
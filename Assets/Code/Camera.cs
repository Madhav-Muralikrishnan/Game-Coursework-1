using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    public Player player;
    public float multiplierUp = 5f;
    public float multiplierLeftRight = 10f;
    private float mouseX; 
    private float mouseY;
    public float cameraVerticalRotation = 0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * multiplierLeftRight;
        mouseY = Input.GetAxisRaw("Mouse Y") * multiplierUp;

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        player.transform.Rotate(Vector3.up * mouseX);
    }

}
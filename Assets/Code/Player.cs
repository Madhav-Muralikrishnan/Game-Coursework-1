using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5;
    public float jumpForce = 1f;
    public GameController gameController;
    private Rigidbody rigidBody;
    private float playerHeight = 2f;
    private bool moving = false;

	// Start is called before the first frame update
	void Start()
	{
        rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
    {
        moving = false;
        KeyPressUpdate();
        SlowMoUpdate();
    }

    private void KeyPressUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * movementSpeed * Time.deltaTime;
            moving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * movementSpeed * Time.deltaTime;
            moving = true;
        }

        var isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            moving = true;
        }
    }

    private void SlowMoUpdate()
    {
        if (!moving && gameController.slowMoTimer < gameController.slowMoTimerMax)
		{
			gameController.slowMoTimer += Time.deltaTime * gameController.regenSlowMoSpeed;
		}

		if (gameController.slowMoTimer != 0)
		{
			gameController.isSlowMo = Input.GetKey(KeyCode.Mouse0);

			if (gameController.isSlowMo)
			{
				gameController.slowMoTimer -= Time.deltaTime;
				if (gameController.slowMoTimer < 0)
				{
					gameController.slowMoTimer = 0;
				}
			}
		}
		else
		{
			gameController.isSlowMo = false;
		}
    }

    public void Die()
	{
		Debug.Log("You Died!");
		gameController.Respawn();
	}
}
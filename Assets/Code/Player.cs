using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movementSpeed = 5;
	public float jumpForce = 1f;
	public float jumpGravity = 7f;
	public AudioSource deathSound;
	private GameController gameController;
	private Rigidbody rigidBody;
	private float playerHeight = 2f;
	private bool moving = false;
	private bool isGrounded;

	// Start is called before the first frame update
	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);
		moving = false;
		KeyPressUpdate();
		SlowMoUpdate();
		ControlDrag();
	}

	private void KeyPressUpdate()
	{
		if (Input.GetKey(KeyCode.W))
		{
			rigidBody.velocity += transform.forward * movementSpeed * Time.deltaTime;
			moving = true;
		}
		if (Input.GetKey(KeyCode.S))
		{
			rigidBody.velocity -= transform.forward * movementSpeed * Time.deltaTime;
			moving = true;
		}

		if (Input.GetKey(KeyCode.D))
		{
			rigidBody.velocity += transform.right * movementSpeed * Time.deltaTime;
			moving = true;
		}
		if (Input.GetKey(KeyCode.A))
		{
			rigidBody.velocity -= transform.right * movementSpeed * Time.deltaTime;
			moving = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
				if (!gameController.powerUp2Active)
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
		deathSound.Play();
		gameController.Respawn();
	}

	private void ControlDrag()
	{
		if(rigidBody.velocity.y < 0){
			rigidBody.AddForce(Vector3.down * jumpGravity, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter(Collision collider)
	{
		if(collider.gameObject.tag == "Wall"){
			rigidBody.velocity = Vector3.zero;
		}
	}
}
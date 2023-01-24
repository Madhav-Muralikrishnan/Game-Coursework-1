using UnityEngine;

public class Player : MonoBehaviour
{
	public float movementSpeed = 5;
	
	[Header("Objects")]
	[SerializeField]
	private GameController gameController;
	[SerializeField]
	private Rigidbody rigidBody;

	[Header("Jumping")]
	[SerializeField]
	private float jumpForce = 1f;
	[SerializeField]
	private float jumpGravity = 7f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource deathSound;

	private readonly float playerHeight = 2f;
	private readonly float wallDistance = 1f;

	private bool moving;
	private bool isGrounded;
	private bool isOnForwardWall;
	private bool isOnBackwardWall;
	private bool isOnLeftWall;
	private bool isOnRightWall;

	private void Update()
	{
		if (gameController.dead || gameController.finish)
			return;
			
		isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);
		
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			moving = true;
		}

	}

	private void FixedUpdate()
	{
		moving = false;

		if (gameController.dead || gameController.finish)
			return;
			
		GetRayCasts();
		KeyPressUpdate();
		SlowMoUpdate();
		ControlDrag();
	}

	private void GetRayCasts()
	{
		isOnLeftWall = Physics.Raycast(transform.position, -transform.right, wallDistance);
		isOnRightWall = Physics.Raycast(transform.position, transform.right, wallDistance);
		isOnForwardWall = Physics.Raycast(transform.position, transform.forward, wallDistance);
		isOnBackwardWall = Physics.Raycast(transform.position, -transform.forward, wallDistance);
	}

	private void KeyPressUpdate()
	{
		if (Input.GetKey(KeyCode.W) && !isOnForwardWall)
		{
			VelocityChanges(transform.forward, true);
		}
		if (Input.GetKey(KeyCode.S) && !isOnBackwardWall)
		{
			VelocityChanges(transform.forward, false);
		}
		if (Input.GetKey(KeyCode.D) && !isOnRightWall)
		{
			VelocityChanges(transform.right, true);
		}
		if (Input.GetKey(KeyCode.A) && !isOnLeftWall)
		{
			VelocityChanges(transform.right, false);
		}
	}

	private void VelocityChanges(Vector3 direction, bool positive)
	{
		if(positive)
		{
			rigidBody.velocity += movementSpeed * Time.deltaTime * direction;
		}
		else
		{
			rigidBody.velocity -= movementSpeed * Time.deltaTime * direction;
		}

		moving = true;
	}

	private void SlowMoUpdate()
	{
		if (!moving && gameController.slowMoTimer < gameController.slowMoTimerMax && isGrounded)
			gameController.slowMoTimer += Time.deltaTime * gameController.regenSlowMoSpeed;
		
		if (gameController.slowMoTimer != 0)
		{
			UseSlowMo();
		}
		else
		{
			gameController.isSlowMo = false;
		}
	}

	private void UseSlowMo()
	{
		gameController.isSlowMo = Input.GetKey(KeyCode.Mouse0) && gameController.leftClick && gameController.slowMoTimer - Time.deltaTime > 0;

		if (gameController.isSlowMo)
		{
			if (!(gameController.powerUp2Active || gameController.unlimitedSloMo))
				gameController.slowMoTimer -= Time.deltaTime;

			if (gameController.slowMoTimer < 0)
				gameController.slowMoTimer = 0;
		}
	}

	public void Die()
	{
		if (gameController.dead || gameController.finish)
			return;

		Debug.Log("You Died!");
		deathSound.Play();
		gameController.Respawn();
	}

	private void ControlDrag()
	{
		if(rigidBody.velocity.y < 0)
			rigidBody.AddForce(Vector3.down * jumpGravity, ForceMode.Acceleration);
	}
}
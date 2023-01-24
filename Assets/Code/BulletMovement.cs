using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	public float movementSpeed = 5.0f;
	public float slowedSpeed = 0.5f;

	[SerializeField]
	private Rigidbody rigidBody;

	private GameController gameController;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		rigidBody.AddForce(transform.forward * movementSpeed);
	}

	void FixedUpdate()
	{
		if (gameController.finish)
			return;
			
		rigidBody.velocity = Vector3.zero;

		var speed = gameController.isSlowMo ? slowedSpeed : movementSpeed;
		rigidBody.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
}

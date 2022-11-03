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

    public float groundDrag = 6f;
    public float airDrag = 2f;
    bool isGrounded;

	// Start is called before the first frame update
	void Start()
	{
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
		gameController.Respawn();
	}

    void ControlDrag()
    {
        if(isGrounded){
            rigidBody.drag = groundDrag;
        }
        else
        {
           rigidBody.drag = airDrag; 
        }
    }

    void OnCollisionEnter(Collision collider)
	{
		if(collider.gameObject.tag == "Wall"){
			rigidBody.velocity = Vector3.zero;
		}
	}
}
//using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     public float movementSpeed = 5;
//     public float jumpForce = 1f;
//     public GameController gameController;
//     private Rigidbody rigidBody;
//     private float playerHeight = 2f;
//     private bool moving = false;

//     public float groundDrag = 6f;
//     public float airDrag = 2f;
//     bool isGrounded;

// 	// Start is called before the first frame update
// // 	void Start()
// // 	{
// //         rigidBody = GetComponent<Rigidbody>();
// // 	}

// // 	private void Update()
// //     {
// //         isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);
// //         //rigidBody.velocity = new Vector3(0f,0f,0f);
// //         moving = false;
// //         KeyPressUpdate();
// //         SlowMoUpdate();
// //         //ControlDrag();

// //         /*float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
// //         float vertical = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

// //         transform.Translate(horizontal, 0, vertical);

// //         if (Input.GetKeyDown(Keycode.Space) && isGrounded)
// //         {
// //             rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
// //             moving = true;
// //         }*/

// //     }

// //     private void KeyPressUpdate()
// //     {
// //         //float moveX = Input.GetAxisRaw("Horizontal");
// //         //float moveZ = Input.GetAxisRaw("Vertical");
// //         //rigidBody.velocity = new Vector3(moveX, rigidBody.velocity.y,moveZ) * movementSpeed;
// //         if (Input.GetKey(KeyCode.W))
// //         {
// //             if(Physics.OverlapCapsule(new Vector3(transform.position.x,1.5f,transform.position.z), new Vector3(transform.position.x,0.7f,transform.position.z), 0.5f).Length == 0)
// //             {rigidBody.MovePosition(transform.forward * movementSpeed * Time.deltaTime);
// //                 //rigidBody.velocity += transform.forward * movementSpeed * Time.deltaTime;
// //                 moving = true;
// //             }
// //         }
// //         if (Input.GetKey(KeyCode.S))
// //         {
// //             if(Physics.OverlapCapsule(new Vector3(transform.position.x,1.5f,transform.position.z), new Vector3(transform.position.x,0.7f,transform.position.z), 0.5f).Length == 0)
// //             {
// //             Debug.Log("physics");
// //             rigidBody.MovePosition(transform.forward * movementSpeed * Time.deltaTime);
// //             //transform.position -= transform.forward * movementSpeed * Time.deltaTime;
// //             moving = true;
// //             }
// //         }

// //         if (Input.GetKey(KeyCode.D))
// //         {
// //             if(Physics.OverlapCapsule(new Vector3(transform.position.x,1.5f,transform.position.z), new Vector3(transform.position.x,0.7f,transform.position.z), 0.5f).Length == 0)
// //             {rigidBody.MovePosition(transform.position += transform.right * movementSpeed * Time.deltaTime);
// //             //rigidBody.velocity += transform.right * movementSpeed * Time.deltaTime;
// //             moving = true;
// //             }
// //         }
// //         if (Input.GetKey(KeyCode.A))
// //         {
// //             if(Physics.OverlapCapsule(new Vector3(transform.position.x,1.5f,transform.position.z), new Vector3(transform.position.x,0.7f,transform.position.z), 0.5f).Length == 0)
// //             {rigidBody.MovePosition(transform.position -= transform.right * movementSpeed * Time.deltaTime);
// //             //rigidBody.velocity -= transform.right * movementSpeed * Time.deltaTime;
// //             moving = true;
// //             }
// //         }
// //         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
// //         {
// //             rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
// //             moving = true;
// //         }

        
// //     }

// //     void ControlDrag()
// //     {
// //         if(isGrounded){
// //             rigidBody.drag = groundDrag;
// //         }
// //         else
// //         {
// //            rigidBody.drag = airDrag; 
// //         }
// //     }

// //     private void SlowMoUpdate()
// //     {
// //         if (!moving && gameController.slowMoTimer < gameController.slowMoTimerMax)
// // 		{
// // 			gameController.slowMoTimer += Time.deltaTime * gameController.regenSlowMoSpeed;
// // 		}

// // 		if (gameController.slowMoTimer != 0)
// // 		{
// // 			gameController.isSlowMo = Input.GetKey(KeyCode.Mouse0);

// // 			if (gameController.isSlowMo)
// // 			{
// // 				gameController.slowMoTimer -= Time.deltaTime;
// // 				if (gameController.slowMoTimer < 0)
// // 				{
// // 					gameController.slowMoTimer = 0;
// // 				}
// // 			}
// // 		}
// // 		else
// // 		{
// // 			gameController.isSlowMo = false;
// // 		}
// //     }

// //     public void Die()
// // 	{
// // 		Debug.Log("You Died!");
// // 		gameController.Respawn();
// // 	}
// // }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixamoAnim : MonoBehaviour
{
	private Animator animator;
	public GameController gameController;
	public Player player;

	// Start is called before the first frame update
	void Start()
	{
	   animator = GetComponent<Animator>(); 
	}

	// Update is called once per frame
	void Update()
	{
		//animator.SetBool("IsJumping", player.isGrounded);
		animator.SetBool("ForwardWalking", Input.GetKey(KeyCode.W));
		animator.SetBool("BackwardsWalking", Input.GetKey(KeyCode.S));
		animator.SetBool("LeftWalking", Input.GetKey(KeyCode.A));
		animator.SetBool("RightWalking", Input.GetKey(KeyCode.D));
		animator.SetBool("IsDead", gameController.dead);
		animator.SetBool("IsFinished", gameController.finish);
	}
}

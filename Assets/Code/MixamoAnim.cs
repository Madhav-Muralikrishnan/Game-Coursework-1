using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixamoAnim : MonoBehaviour
{
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
	   animator = GetComponent<Animator>(); 
	}

	// Update is called once per frame
	void Update()
	{
		animator.SetBool("ForwardWalking", Input.GetKey(KeyCode.W));
		animator.SetBool("BackwardsWalking", Input.GetKey(KeyCode.S));
		animator.SetBool("LeftWalking", Input.GetKey(KeyCode.A));
		animator.SetBool("RightWalking", Input.GetKey(KeyCode.D));
	}
}

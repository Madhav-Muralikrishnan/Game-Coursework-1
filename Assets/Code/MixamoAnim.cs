using UnityEngine;

public class MixamoAnim : MonoBehaviour
{
	[SerializeField]
	private GameController gameController;
	[SerializeField]
	private Animator animator;

	void Update()
	{
		animator.SetBool("ForwardWalking", Input.GetKey(KeyCode.W));
		animator.SetBool("BackwardsWalking", Input.GetKey(KeyCode.S));
		animator.SetBool("LeftWalking", Input.GetKey(KeyCode.A));
		animator.SetBool("RightWalking", Input.GetKey(KeyCode.D));
		animator.SetBool("IsDead", gameController.dead);
		animator.SetBool("IsFinished", gameController.finish);
	}
}

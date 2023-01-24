using UnityEngine;

public class BulletCollision : MonoBehaviour
{
	private Player player;

	void Start()
	{
		player = FindObjectOfType<Player>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
			player.Die();

		gameObject.SetActive(false);
	}
}

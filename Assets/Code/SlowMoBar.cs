using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoBar : MonoBehaviour
{
	public Image mask;

	private GameController gameController;

	// Start is called before the first frame update
	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	// Update is called once per frame
	void Update()
	{
		mask.fillAmount = (float)gameController.slowMoTimer / (float)gameController.slowMoTimerMax;
	}
}

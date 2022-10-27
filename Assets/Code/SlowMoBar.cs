using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoBar : MonoBehaviour
{
	public GameController gameController;
	public Image mask;
	private float maximum;

	// Start is called before the first frame update
	void Start()
	{
		maximum = (float)gameController.slowMoTimerMax;
	}

	// Update is called once per frame
	void Update()
	{
		var fillAmount = (float)gameController.slowMoTimer / (float)gameController.slowMoTimerMax;
		mask.fillAmount = fillAmount;
	}
}

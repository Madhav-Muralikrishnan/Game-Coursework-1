using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoBar : MonoBehaviour
{
	public ThirdPersonCamera player;
	public Image mask;
	private float maximum;

	// Start is called before the first frame update
	void Start()
	{
		maximum = (float)player.slowMoTimerMax;
	}

	// Update is called once per frame
	void Update()
	{
		var fillAmount = (float)player.slowMoTimer / (float)player.slowMoTimerMax;
		mask.fillAmount = fillAmount;
	}
}

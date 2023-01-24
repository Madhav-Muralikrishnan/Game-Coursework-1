using UnityEngine;
using UnityEngine.UI;

public class SlowMoBar : MonoBehaviour
{
	[SerializeField]
	private Image mask;
	[SerializeField]
	private GameController gameController;

	void Update()
	{
        mask.fillAmount = gameController.slowMoTimer / gameController.slowMoTimerMax;
	}
}

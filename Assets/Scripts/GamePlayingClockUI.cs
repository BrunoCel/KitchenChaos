using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameManager.instance.GetGamePlayingTimerNormalized();
        if (GameManager.instance.IsGamePlaying())
        {

            if (timerImage.fillAmount >= 0.75f)
            {
                timerImage.color = Color.red;
            }
        }
    }

}

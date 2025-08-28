using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private int countDownNumber;
    private int previousCountDownNumber;

    private const string countDownAnimationTrigger = "NumberPopUp";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.OnStateGhanged += OnGameStateGhanged;
        Hide();
    }

    private void OnGameStateGhanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameCountingDown())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countDownNumber = Mathf.CeilToInt(GameManager.instance.GetCountdownStartTimer());
        countdownText.text = countDownNumber.ToString();

        if (countDownNumber != previousCountDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            animator.SetTrigger(countDownAnimationTrigger);
            SoundManager.instance.PlayCountDownSound();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}

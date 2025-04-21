using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;

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
        countdownText.text = Mathf.Ceil(GameManager.instance.GetCountdownStartTimer()).ToString();
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

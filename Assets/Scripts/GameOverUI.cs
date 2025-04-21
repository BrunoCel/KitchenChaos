using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipesDeliverdCountText;

    private void Start()
    {
        GameManager.instance.OnStateGhanged += OnGameStateGhanged;
        Hide();
    }

    private void OnGameStateGhanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameOver())
        {
            Show();
            RecipesDeliverdCountText.text = GameManager.instance.GetRecipesDelivered().ToString();
        }
        else
        {
            Hide();
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

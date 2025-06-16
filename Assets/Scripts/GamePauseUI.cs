using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { GameManager.instance.TogglePauseGame(); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.SceneName.MainMenuScene); });
        optionsButton.onClick.AddListener(() => { OptionsUI.Instance.Show(); });
    }
    private void Start()
    {
        hide();
        GameManager.instance.OnGamePaused += Gamemanager_OnGamePaused;
        GameManager.instance.OnGameUnpaused += Gamemanager_OnGameUnpaused;
    }

    private void Gamemanager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        hide();
    }

    private void Gamemanager_OnGamePaused(object sender, System.EventArgs e)
    {
       show();
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide() 
    {
        gameObject.SetActive(false);
    }
}

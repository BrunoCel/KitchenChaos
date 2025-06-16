using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button SFXButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;
        SFXButton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolume();
            UPdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeVolume();
            UPdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            Hide();        
        });
    }

    private void Start()
    {
        GameManager.instance.OnGamePaused += GameManager_OnGamePaused;
        Hide();
        UPdateVisual();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void UPdateVisual()
    {
        sfxText.text ="Sound effects : " + Mathf.Round(SoundManager.instance.GetVolume()* 10f);
        musicText.text = "Music : " + Mathf.Round(MusicManager.instance.GetVolume() * 10f);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

}

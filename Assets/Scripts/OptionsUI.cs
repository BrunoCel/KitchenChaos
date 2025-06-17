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
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    

    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

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

        moveUpText.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveUp);
        moveDownText.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveDown);
        moveLeftText.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveLeft);
        moveRightText.text = GameInput.instance.GetBindingText (GameInput.Bindings.MoveRight);
        interactText.text = GameInput.instance.GetBindingText(GameInput.Bindings.Interact);
        interactAltText.text = GameInput.instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        pauseText.text = GameInput.instance.GetBindingText(GameInput.Bindings .Pause);
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

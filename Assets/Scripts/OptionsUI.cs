using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using System;

public class OptionsUI : MonoBehaviour
{
    
    public static OptionsUI Instance { get; private set; }

    private Action onCloseButtonAction;

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
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;


    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamePadinteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Transform pressToRebindVisualUI;

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
            onCloseButtonAction();
            Hide();      
        });

        

        moveUpButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.MoveUp); });

        moveDownButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.MoveDown); });

        moveLeftButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.MoveLeft); });

        moveRightButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.MoveRight); });


        interactButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Interact); });

        interactAltButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.InteractAlternate); });

        pauseButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Pause); });

        gamepadInteractButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_Interact); });

        gamepadInteractAltButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_InteractAlternate); });

        gamepadPauseButton.onClick.AddListener(() => { RebindKey(GameInput.Bindings.Gamepad_Pause); });
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
        gamePadinteractText.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        this.gameObject.SetActive(true);
        SFXButton.Select();
    }

    public void Hide()
    {
        
        this.gameObject.SetActive(false);
        
    }

    private void ShowRebindFeedbackTextUI()
    {
        pressToRebindVisualUI.gameObject.SetActive(true);
    }

    private void HideRebindFeedbackTextUI()
    {
        pressToRebindVisualUI.gameObject.SetActive(false);
    }

    private void RebindKey(GameInput.Bindings binding)
    {
        ShowRebindFeedbackTextUI();
        GameInput.instance.RebindingBindings(binding, () =>
        {
            HideRebindFeedbackTextUI();
            UPdateVisual();
        }
        );
    }
}

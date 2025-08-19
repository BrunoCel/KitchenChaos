using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    //keyboard keys
    [SerializeField] private TextMeshProUGUI moveUpKey;
    [SerializeField] private TextMeshProUGUI moveDownKey;
    [SerializeField] private TextMeshProUGUI moveLeftKey;
    [SerializeField] private TextMeshProUGUI moveRightKey;
    [SerializeField] private TextMeshProUGUI interactKey;
    [SerializeField] private TextMeshProUGUI altIteractKey;
    [SerializeField] private TextMeshProUGUI pauseKey;

    // gamepad keys
    [SerializeField] private TextMeshProUGUI interactGamepadKey;
    [SerializeField] private TextMeshProUGUI altInteractGamepadKey;
    [SerializeField] private TextMeshProUGUI pauseGamepadKey;

    private void Start()
    {
        GameInput.instance.OnbindingRebinding += Instance_OnbindingRebinding;
        UpdateVisuals();
        Show();
    }

    private void Instance_OnbindingRebinding(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        moveUpKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveUp);
        moveDownKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveDown);
        moveLeftKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveLeft);
        moveRightKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.MoveRight);
        interactKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.Interact);
        altIteractKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        pauseKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.Pause);
        interactGamepadKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        altInteractGamepadKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        pauseGamepadKey.text = GameInput.instance.GetBindingText(GameInput.Bindings.Gamepad_Pause);
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

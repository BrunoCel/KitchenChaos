using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public static GameInput instance { get; private set; }

    private const string PLAYER_PREFS_BINDINGS = "InputBinding";

  private PlayerInputActions playerInputActions;
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  public event EventHandler OnPauseAction;

  public enum Bindings
    {
        MoveUp, 
        MoveDown, 
        MoveLeft, 
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
    }

  private void Awake()
  {
        
    instance = this;
    playerInputActions= new PlayerInputActions();

    if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
    {
        playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
    }
        
    playerInputActions.Player.Enable();

    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    playerInputActions.Player.Pause.performed += Pause_performed;

    Debug.Log(GetBindingText(Bindings.Interact));
       
  }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    public void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

  private void InteractAlternate_performed(InputAction.CallbackContext obj)
  {
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    
    if (Input.GetKey(KeyCode.D))
    {
      inputVector.x = 1;
    }
    
    inputVector = inputVector.normalized;
    
    return inputVector;
  }

  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    // caso tenha sub invoke(execute) o evento
      OnInteractAction?.Invoke(this, EventArgs.Empty);
    
  }

    public string GetBindingText(Bindings binding)
    {
        
        switch (binding) {
            default:

            case Bindings.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Bindings.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Bindings.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Bindings.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();

            case Bindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case Bindings.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Bindings.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();

            

        }
    }

    public void RebindingBindings(Bindings binding, Action onRebound)
    {
        playerInputActions.Player.Move.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:

            case Bindings.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;

            case Bindings.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;

            case Bindings.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;

            case Bindings.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;

            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;

        }

        inputAction.Disable();

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onRebound();

                
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}

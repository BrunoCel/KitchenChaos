using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public static GameInput instance { get; private set; }

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
}

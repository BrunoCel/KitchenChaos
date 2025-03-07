using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  private PlayerInputActions playerInputActions;
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  private void Awake()
  {
    playerInputActions= new PlayerInputActions();
    playerInputActions.Player.Enable();

    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
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
}

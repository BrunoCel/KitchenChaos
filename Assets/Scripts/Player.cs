using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}
    public event EventHandler <OnSelectedCounterChangedEventArgs>OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    
    [SerializeField] private float walkingVelocity = 5.0f;
    [SerializeField] private float playerRadius = .7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayers;
    
    private ClearCounter selectedClearCounter;

    private float _rotationSpeed = 20f;

    private bool isWalking;
    private bool canMove = true;
    
    private Vector3 lastInteractPosition;
    
    
    [SerializeField] private GameInput gameInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
       
            if (selectedClearCounter != null)
            {
                selectedClearCounter.Interact();
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleMovement();
        HandleInteractions();

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector2 = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector2.x, 0, inputVector2.y);

        float moveDistance =walkingVelocity * Time.deltaTime;
        canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        
        if(!canMove)
        {
            // can not move foward 
            //attempt to move on x axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0 , 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                // can not move foward or x axis
                //attempt to move on z axis
                Vector3 moveDirectionZ = new Vector3(0, 0 , moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }
        
        if (canMove)
        {
            transform.position +=moveDirection * moveDistance;
        }
        
        
        
        isWalking = moveDirection != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward , moveDirection, Time.deltaTime * _rotationSpeed );

    }

    private void HandleInteractions()
    {
        Vector2 inputVector2 = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector2.x, 0, inputVector2.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractPosition = moveDirection;
        }

        if (Physics.Raycast(transform.position,lastInteractPosition, out RaycastHit raycastHit, interactDistance, interactableLayers))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedClearCounter)
                {
                    SetSelectedCounter(clearCounter); 
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
             SetSelectedCounter(null);
        }

        void SetSelectedCounter(ClearCounter selectedCounter)
        {
            this.selectedClearCounter = selectedCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedClearCounter });
        }
    }
}

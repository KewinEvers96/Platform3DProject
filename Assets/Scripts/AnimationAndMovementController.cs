 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    // Start is called before the first frame update

    #region Player_Input_Values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15.0f;
    #endregion

    void Awake()
    {
        playerInput = new PlayerInput();

        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started +=  OnInputPlayerMovement;
        
        playerInput.CharacterControls.Move.canceled += OnInputPlayerMovement;
        
        playerInput.CharacterControls.Move.performed += OnInputPlayerMovement;

        playerInput.CharacterControls.Run.started += OnInputRun;

        playerInput.CharacterControls.Run.canceled += OnInputRun;

        playerInput.CharacterControls.Run.performed += OnInputRun;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }


    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if( isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if(!isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", false);
        }

    }

    void OnInputPlayerMovement(InputAction.CallbackContext context) 
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * 3.0f;
        currentRunMovement.z = currentMovementInput.y * 3.0f;
        isMovementPressed = currentMovement.x != 0 || currentMovement.z != 0;
    }

    void OnInputRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();

        if (isRunPressed )
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        characterController.Move(currentMovement * Time.deltaTime);
    }
    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
     void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}

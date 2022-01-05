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

    #region Gravity_Control_Constants
    //Gravity constant control
    float gravity = -9.8f;
    float groundedGravity = -.05f;
    #endregion

    #region Player_Input_Values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15.0f;
    #endregion

    #region Player_Input_Jump_Control_Variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1.0f;
    float maxJumpTime= 0.5f;
    bool isJumping = false;
    #endregion

    void Awake()
    {
        playerInput = new PlayerInput();

        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += OnInputPlayerMovement;

        playerInput.CharacterControls.Move.canceled += OnInputPlayerMovement;

        playerInput.CharacterControls.Move.performed += OnInputPlayerMovement;

        playerInput.CharacterControls.Run.started += OnInputRun;

        playerInput.CharacterControls.Run.canceled += OnInputRun;

        playerInput.CharacterControls.Jump.started += OnInputJump;

        playerInput.CharacterControls.Jump.canceled += OnInputJump;

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity * .5f;
            currentMovement.y = initialJumpVelocity * .5f;
        }else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
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

    void OnInputJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
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

        handleGravity();
        handleJump();
    }
    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
     void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}

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
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15.0f;
    #endregion

    #region Player_Input_Jump_Control_Variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2.0f;
    float maxJumpTime= 0.75f;
    bool isJumping = false;
    bool isJumpAnimating=false;
    bool canDoubleJump=true;
   int jumpCount=0;
   Dictionary<int,float> initialJumpVelocities= new Dictionary<int,float>();
   Dictionary<int,float> jumpGravities= new Dictionary<int,float>();
   Coroutine currentJumpResetRoutine=null;
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
        float secondJumpGravity=(-2 * (maxJumpHeight+1)) / Mathf.Pow((timeToApex*1.25f), 2);
        float secondJumpInitialVelocity=(2 * (maxJumpHeight+1)) / (timeToApex*1.25f);
        float thirdJumpGravity=(-2 * (maxJumpHeight+2)) / Mathf.Pow((timeToApex*1.5f), 2);
        float thirdJumpInitialVelocity=(2 * (maxJumpHeight+2)) / (timeToApex*1.5f);
        float doubleJumpGravity=(-2 * (maxJumpHeight+4)) / Mathf.Pow((timeToApex*1.5f), 2);
        float doubleJumpInitialVelocity=(2 * (maxJumpHeight+4)) / (timeToApex*1.5f);

        initialJumpVelocities.Add(1,initialJumpVelocity);
        initialJumpVelocities.Add(2,secondJumpInitialVelocity);
        initialJumpVelocities.Add(3,thirdJumpInitialVelocity);
        initialJumpVelocities.Add(4,doubleJumpInitialVelocity);
        jumpGravities.Add(0,gravity);
        jumpGravities.Add(1,gravity);
        jumpGravities.Add(2,secondJumpGravity);
        jumpGravities.Add(3,thirdJumpGravity);
        jumpGravities.Add(4,doubleJumpGravity);
    }
    //IEnumerator jumpResetRoutine(){
    //   yield return new WaitForSeconds(0.5f);
    //    jumpCount=0;
  //  }
    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
          //  if(jumpCount<3 && currentJumpResetRoutine!= null){
           //     StopCoroutine(currentJumpResetRoutine);
           // }
            canDoubleJump=true;
            animator.SetBool("isJumping",true);
            isJumpAnimating=true;
            isJumping = true;
            jumpCount+=1;
            animator.SetInteger("jumpCount",jumpCount);
            currentMovement.y = initialJumpVelocities[jumpCount] ;
            appliedMovement.y = initialJumpVelocities[jumpCount] ;
        }
        else if (isJumpPressed && isJumping && !characterController.isGrounded && canDoubleJump)
        {
            canDoubleJump=false;
            
            
            currentMovement.y = initialJumpVelocities[4] ;
            appliedMovement.y = initialJumpVelocities[4] ; 
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            canDoubleJump=true;
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
            if(isJumpAnimating){
            animator.SetBool("isJumping",false);
           // currentJumpResetRoutine=  StartCoroutine(jumpResetRoutine());
            
            if(jumpCount==3){
                jumpCount=0;
                animator.SetInteger("jumpCount",jumpCount);
            }
            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y= currentMovement.y + (jumpGravities[jumpCount] * fallMultiplier * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;
            
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (jumpGravities[jumpCount] * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;
            
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
            appliedMovement.x=currentRunMovement.x;
            appliedMovement.z=currentRunMovement.z;
            
        }
        else
        {
            appliedMovement.x=currentMovement.x;
            appliedMovement.z=currentMovement.z;
            
        }
        characterController.Move(appliedMovement * Time.deltaTime);

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

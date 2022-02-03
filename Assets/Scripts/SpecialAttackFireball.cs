using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SpecialAttackFireball : MonoBehaviour
{
    PlayerInput playerInput;
    
    CharacterController characterController;
    public GameObject energyballPFAB;
    public GameObject fireballPFAB;

    public Transform spawnPointTRAN;
    public Transform enemyTran;

    public float chargeTime;
    public float fireballSpeed;

    Animator characterANIM;
    bool isAttacking = false; 
    bool isAttackPressed = false;
    GameObject energyballOBJ;
    GameObject fireballOBJ;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterANIM=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttackPressed){
            characterANIM.SetBool("isAttacking",true);
            energyballOBJ=Instantiate(energyballPFAB,spawnPointTRAN.position,spawnPointTRAN.rotation);
            energyballOBJ.transform.SetParent(spawnPointTRAN);
        }
        else{
            characterANIM.SetBool("isAttacking",false);
        }
    }

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.CharacterControls.Attack.started += OnInputAttack;
        playerInput.CharacterControls.Attack.canceled += OnInputAttack;
    }
    

    void OnInputAttack(InputAction.CallbackContext context)
    {
        Debug.Log("holi");
        isAttackPressed = context.ReadValueAsButton();
    }
}

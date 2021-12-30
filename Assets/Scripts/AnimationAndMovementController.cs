using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInputs playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput=new PlayerInputs();
        playerInput.CharacterControls.Move.started+=context=> {Debug.Log(context.ReadValue<Vector2>());};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
     void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}

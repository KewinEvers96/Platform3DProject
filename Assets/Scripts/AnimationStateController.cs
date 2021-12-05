using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity=0.0f;
    public float acceleration=0.1f;
    public float deceleration=0.1f;
    int VelocityHash;
    int isWalkingHash;
    int isRunningHash;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        //animation with booleans
        isWalkingHash=Animator.StringToHash("isWalking");
        isRunningHash=Animator.StringToHash("isRunning");
        //animations with float
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        //animation with booleans
      /*  bool walkPressed=Input.GetKey("w");

        bool runPressed=Input.GetKey("left shift");
        bool isWalking= animator.GetBool(isWalkingHash);
        bool isRunning= animator.GetBool(isRunningHash);
        if(!isWalking && walkPressed){
            animator.SetBool(isWalkingHash,true);
        }
        if(isWalking && !walkPressed){
            animator.SetBool(isWalkingHash,false);
        }
        if(!isRunning && (walkPressed && runPressed)){
            animator.SetBool(isRunningHash,true);
        }
        if(isRunning && (!walkPressed||!runPressed)){
            animator.SetBool(isRunningHash,false);
        }*/

        //animation with float
        bool walkPressed=Input.GetKey("w");
        bool runPressed=Input.GetKey("left shift");
        if(walkPressed && velocity <1.0f){
            velocity += Time.deltaTime*acceleration;
        }
        if(!walkPressed && velocity>0.0f){
            velocity -= Time.deltaTime*deceleration;
        }
        if(!walkPressed && velocity<0.0f){
            velocity =0.0f;
        }
        animator.SetFloat(VelocityHash,velocity);
    }
}

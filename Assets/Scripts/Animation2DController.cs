using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2DController : MonoBehaviour
{
    Animator animator;
    int VelocityXHash;
    int VelocityZHash;
    float velocityZ=0.0f;
    float velocityX=0.0f;
    public float acceleration=2.0f;
    public float deceleration=2.0f;
    public float maximumWalkVelocity=0.5f;
    public float maximumRunVelocity=2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
    }


    void changeVelocity(bool walkForwardPressed,bool walkLeftPressed,bool walkRightPressed,bool walkBackwardPressed,bool runPressed,float currentMaxVelocity){
        //accelerate forward
        if(walkForwardPressed && velocityZ< currentMaxVelocity ){
            velocityZ+=Time.deltaTime*acceleration;
        }
        //decelerate forward
        if(!walkForwardPressed && velocityZ > 0.0f ){
            velocityZ-=Time.deltaTime*deceleration;
        }

        //accelerate backward
        if (walkBackwardPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        //decelerate backward
        if (!walkBackwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        //reset velocity backward
        if (!walkForwardPressed && !walkBackwardPressed && velocityZ != 0.0f && (velocityZ > -currentMaxVelocity && velocityZ < currentMaxVelocity))
        {
            velocityZ = 0.0F;
        }


        //accelerate left
        if (walkLeftPressed && velocityX >-currentMaxVelocity ){
            velocityX-=Time.deltaTime*acceleration;
        }
        //accelerate right
        if(walkRightPressed && velocityX<currentMaxVelocity ){
            velocityX+=Time.deltaTime*acceleration;
        }
        
        //decelerate left
        if(!walkLeftPressed && velocityX < 0.0f ){
            velocityX+=Time.deltaTime*deceleration;
        }
        //decelerate right
        if(!walkRightPressed && velocityX > 0.0f ){
            velocityX-=Time.deltaTime*deceleration;
        }
        //reset x velocity
        if(!walkRightPressed && !walkLeftPressed && velocityX != 0.0f && (velocityX>-currentMaxVelocity && velocityX<currentMaxVelocity)){
            velocityX=0.0f;
        }
    }

    void lockOrResetVelocity(bool walkForwardPressed,bool walkLeftPressed,bool walkRightPressed,bool walkBackwardPressed,bool runPressed,float currentMaxVelocity){
// forward lock
        if(walkForwardPressed && runPressed && velocityZ > currentMaxVelocity ){
            velocityZ=currentMaxVelocity;
        }
        else if(walkForwardPressed  && velocityZ > currentMaxVelocity ){
            velocityZ-=Time.deltaTime*deceleration;
            if( velocityZ > currentMaxVelocity && velocityZ<(currentMaxVelocity + 0.05f)){
            velocityZ=currentMaxVelocity;
        }
        }
        else if(walkForwardPressed  && velocityZ < currentMaxVelocity && velocityZ>(currentMaxVelocity -0.05f)){
            velocityZ=currentMaxVelocity;
        }

        // Backward lock
        if (walkBackwardPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (walkBackwardPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if (walkBackwardPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        // left LOCKING
        if (walkLeftPressed && runPressed && velocityX < -currentMaxVelocity ){
            velocityX= -currentMaxVelocity;
        }
        else if(walkLeftPressed  && velocityX < -currentMaxVelocity ){
            velocityX+=Time.deltaTime*deceleration;
            if( velocityX < -currentMaxVelocity && velocityX >(-currentMaxVelocity - 0.05f)){
            velocityX=-currentMaxVelocity;
        }
        }
        else if(walkLeftPressed  && velocityX > -currentMaxVelocity && velocityX<(-currentMaxVelocity +0.05f)){
            velocityX=-currentMaxVelocity;
        }



        // right locking
        if (walkRightPressed && runPressed && velocityX > currentMaxVelocity ){
            velocityX=currentMaxVelocity;
        }
        else if(walkRightPressed  && velocityX > currentMaxVelocity ){
            velocityX-=Time.deltaTime*deceleration;
            if( velocityX > currentMaxVelocity && velocityX<(currentMaxVelocity + 0.05f)){
            velocityX=currentMaxVelocity;
        }
        }
        else if(walkRightPressed  && velocityX < currentMaxVelocity && velocityX>(currentMaxVelocity -0.05f)){
            velocityX=currentMaxVelocity;
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool walkForwardPressed=Input.GetKey(KeyCode.W);
        bool walkLeftPressed=Input.GetKey(KeyCode.A);
        bool walkRightPressed=Input.GetKey(KeyCode.D);
        bool walkBackwardPressed=Input.GetKey(KeyCode.S);
        bool runPressed=Input.GetKey(KeyCode.LeftShift);
        float currentMaxVelocity=runPressed ? maximumRunVelocity:maximumWalkVelocity;
        
        changeVelocity(walkForwardPressed,walkLeftPressed,walkRightPressed,walkBackwardPressed,runPressed,currentMaxVelocity);
        lockOrResetVelocity(walkForwardPressed,walkLeftPressed,walkRightPressed,walkBackwardPressed,runPressed,currentMaxVelocity);
        





        animator.SetFloat(VelocityZHash,velocityZ);
        animator.SetFloat(VelocityXHash,velocityX);
    }
}

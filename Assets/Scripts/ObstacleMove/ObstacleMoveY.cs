using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveY : MonoBehaviour
{

    private bool MoverUp = true;

    [SerializeField]
    float move =2;
    [SerializeField]
    float LimiteUp = 15;
    [SerializeField]
    float LimiteDown = 2;


    void Update()
    { 
        transform.Translate(0,move*Time.deltaTime,0);
        if (transform.position.y>=LimiteUp && MoverUp)
        {
            move=move*(-1);
            MoverUp=false;
            //destroyObject();         
        }else if (transform.position.y<=LimiteDown && MoverUp==false )
        {
            move=move*(-1);
            MoverUp=true;
        }
    }


}


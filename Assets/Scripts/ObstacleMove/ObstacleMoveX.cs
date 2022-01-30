using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveX : MonoBehaviour
{

    private bool MoverRight = true;

    [SerializeField]
    float move =2;
    [SerializeField]
    float LimiteDerecho ;
    [SerializeField]
    float LimiteIzquierdo ;


    void Update()
    { 
        transform.Translate(move*Time.deltaTime,0,0);
        if (transform.position.x>=LimiteDerecho && MoverRight)
        {
            move=move*(-1);
            MoverRight=false;
         
        }else if (transform.position.x<=LimiteIzquierdo && MoverRight==false )
        {
            move=move*(-1);
            MoverRight=true;
        }
    }


}

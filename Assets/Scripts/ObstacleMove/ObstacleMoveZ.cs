using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveZ : MonoBehaviour
{

    private bool MoverZ = true;

    [SerializeField]
    float move =2;
    [SerializeField]
    float LimiteAdelante = 4;
    [SerializeField]
    float LimiteAtras = -4;


    void Update()
    { 
        transform.Translate(0,0,move*Time.deltaTime);
        if (transform.position.z>=LimiteAdelante && MoverZ)
        {
            move=move*(-1);
            MoverZ=false;
            //destroyObject();         
        }else if (transform.position.z<=LimiteAtras && MoverZ==false )
        {
            move=move*(-1);
            MoverZ=true;
        }
    }


}
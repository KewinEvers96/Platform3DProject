using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private PlayerMovement m_MovableAgent;
    private PathContainer m_PathContainer;

    private Transform m_Agent;

    public static bool startPatrol = false;

    Vector3 nextPosition = new Vector3(0,0,0);

    float distance;
    void Start()
    {
        m_MovableAgent = GetComponent<PlayerMovement>();
        m_PathContainer = GetComponent<PathContainer>();
        m_Agent = GetComponent<Transform>();
    }

    void Update()
    {

        distance = Vector3.Distance(nextPosition, m_Agent.position);

        if (PlayerMovement.walk == true && startPatrol == false){

            startPatrol = true;
            nextPosition = m_PathContainer.NextPoint();
            m_MovableAgent.FollowPatrol(nextPosition);

        }

        if (startPatrol == true && distance < 1){
            
            nextPosition = m_PathContainer.NextPoint();
            m_MovableAgent.FollowPatrol(nextPosition);
        }
        
    }
}

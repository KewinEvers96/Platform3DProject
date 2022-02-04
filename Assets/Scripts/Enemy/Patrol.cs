using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private PlayerMovement m_MovableAgent;
    private PathContainer m_PathContainer;

    private Transform m_Agent;

    Vector3 nextPosition;

    float distance;
    public State state;

    void Start()
    {
        
        state = GetComponent<State>();
        m_MovableAgent = GetComponent<PlayerMovement>();
        m_PathContainer = GetComponent<PathContainer>();
        nextPosition = m_PathContainer.NextPoint();
        m_Agent = GetComponent<Transform>();
    }

    void Update()
    {

        distance = Vector3.Distance(nextPosition, m_Agent.position);

        // Debug.Log(gameObject);
        // Debug.Log(distance);
        // Debug.Log(nextPosition);
        
        if (state.walk == true){

            m_MovableAgent.FollowPatrol(nextPosition);
            if (distance <= 1){
                nextPosition = m_PathContainer.NextPoint();
            }

        }
        
    }
}

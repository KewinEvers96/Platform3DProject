using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private PlayerMovement m_MovableAgent;
    private Transform m_Agent;

    float distance;

    [SerializeField] private float distance_follow = 10f;
    [SerializeField] private float distance_figth = 1.5f;    

    public State state;

    void Start()
    {
        state = GetComponent<State>();
        m_MovableAgent = GetComponent<PlayerMovement>();
        m_Agent = GetComponent<Transform>();        
    }

    void Update()
    {
        distance = Vector3.Distance(m_Target.position, m_Agent.position);

        if (m_Target != null && distance >= distance_figth && distance < distance_follow)
        {
            m_MovableAgent.GoTo(m_Target.position);
        }
        else if (m_Target != null && distance > distance_follow){
            state.walk = true;
            state.figth = false;
            state.run = false;
        }
        else if (m_Target != null && distance < distance_figth){
            OnArrive();
        }
    }

    private void OnArrive()
    {
        state.figth = true;
        state.run = false;
        state.walk = false;
    }
}

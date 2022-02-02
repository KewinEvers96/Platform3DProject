using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private PlayerMovement m_MovableAgent;
    private Transform m_Agent;

    float distance;

    void Start()
    {
        m_MovableAgent = GetComponent<PlayerMovement>();
        m_Agent = GetComponent<Transform>();
    }

    void Update()
    {
        distance = Vector3.Distance(m_Target.position, m_Agent.position);
        if (m_Target != null && distance >= 1.5f && distance < 10f)
        {
            m_MovableAgent.GoTo(m_Target.position);
        }
        else if (m_Target != null && distance > 10f){
            PlayerMovement.walk = true;
            PlayerMovement.figth = false;
            PlayerMovement.run = false;
        }
        else if (m_Target != null && distance < 1.5f){
            OnArrive();
        }
    }

    private void OnArrive()
    {
        PlayerMovement.figth = true;
        PlayerMovement.run = false;
        PlayerMovement.walk = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private PlayerMovement m_MovableAgent;

    void Start()
    {
        m_MovableAgent = GetComponent<PlayerMovement>();
    }

    void Update()
    {
       // Debug.Log(Vector3.Distance(m_Target.position, m_MovableAgent.TargetPosition));
        if (m_Target != null && Vector3.Distance(m_Target.position, m_MovableAgent.TargetPosition) > 0.1f)
        {
            m_MovableAgent.GoTo(m_Target.position, OnArrive);
        }
        else if (m_Target != null && Vector3.Distance(m_Target.position, m_MovableAgent.TargetPosition) > 10f){
            PlayerMovement.idle = true;
            PlayerMovement.figth = false;
            PlayerMovement.run = false;
        }
    }

    private void OnArrive()
    {
       // Debug.Log("Arrived");
        PlayerMovement.figth = true;
        PlayerMovement.run = false;
        PlayerMovement.idle = false;
    }
}

using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerMovement : MonoBehaviour
{

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_TargetPosition;
    public Vector3 TargetPosition => m_TargetPosition;
    public Animator animator;

    private Action m_OnArrive;    
    public State state;

    private void Start() {
        state = GetComponent<State>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NavMeshAgent.speed = 2f;
    }

    void Update()
    {
        animator.SetBool("IsMoving", state.run);
        animator.SetBool("IsFighting", state.figth);
        animator.SetBool("isWalking", state.walk);      
    }

    public void GoTo(Vector3 position)
    {
        state.walk = false;
        state.figth = false;
        state.run = true;
        m_NavMeshAgent.isStopped = false;
        m_TargetPosition = position;
        m_NavMeshAgent.SetDestination(position);
    }

    public void FollowPatrol(Vector3 position)
    {
        m_NavMeshAgent.SetDestination(position);
    }

    public void Stop()
    {
        m_NavMeshAgent.isStopped = true;
    }
}

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

    public static bool figth;
    public static bool run;
    public static bool walk;

    private void Start() {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NavMeshAgent.speed = 2f;
    }

    void Update()
    {
        animator.SetBool("IsMoving", run);
        animator.SetBool("IsFighting", figth);
        animator.SetBool("isWalking", walk);      
    }

    public void GoTo(Vector3 position)
    {
        PlayerMovement.walk = false;
        PlayerMovement.figth = false;
        PlayerMovement.run = true;
        m_NavMeshAgent.isStopped = false;
        Patrol.startPatrol = false;
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

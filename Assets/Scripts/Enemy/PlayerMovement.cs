using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerMovement : MonoBehaviour
{

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_TargetPosition;
    public Vector3 TargetPosition => m_TargetPosition;
    public Animator animator;

    public static bool figth;
    public static bool run;
    public static bool idle;

    private void Start() {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NavMeshAgent.speed = 2f;
    }

    void Update()
    {
        animator.SetBool("IsMoving", run);
        animator.SetBool("IsFighting", figth);
        animator.SetBool("Idle", idle);          
    }

    public void GoTo(Vector3 position)
    {
        PlayerMovement.idle = false;
        PlayerMovement.figth = false;
        PlayerMovement.run = true;
        m_NavMeshAgent.isStopped = false;
        m_TargetPosition = position;
        m_NavMeshAgent.SetDestination(position);
    }

    public void Stop()
    {
        m_NavMeshAgent.isStopped = true;
    }
}

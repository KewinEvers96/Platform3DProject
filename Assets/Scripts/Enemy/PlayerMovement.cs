using UnityEngine;
using UnityEngine.AI;
using System;

public enum NavMeshAgentState
{
    Idle,
    Moving,

    Fighting,
}
public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgentState State
    {
        get
        {
            if (m_NavMeshAgent.isStopped || (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance && m_NavMeshAgent.velocity.sqrMagnitude == 0))
                return NavMeshAgentState.Fighting;

            return NavMeshAgentState.Moving;
        }
    }

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_TargetPosition;
    private Action m_OnArrive;

    public Vector3 TargetPosition => m_TargetPosition;
    public Animator animator;

    public static bool figth;
    public static bool run;
    public static bool idle;

    private void Start() {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (m_OnArrive != null)
        {
            if (!m_NavMeshAgent.pathPending && State == NavMeshAgentState.Fighting)
            {
                m_OnArrive();
                m_OnArrive = null;
            }
        }

        Debug.Log("run");
        Debug.Log(run);
        Debug.Log("figth");
        Debug.Log(figth);
        Debug.Log("idle");
        Debug.Log(idle);
        animator.SetBool("IsMoving", run);
        animator.SetBool("IsFighting", figth);
        animator.SetBool("Idle", idle);
        
                
    }

    public void GoTo(Vector3 position, Action onArrive = null)
    {
        PlayerMovement.idle = false;
        PlayerMovement.figth = false;
        PlayerMovement.run = true;
        m_NavMeshAgent.isStopped = false;
        m_OnArrive = onArrive;
        m_TargetPosition = position;
        m_NavMeshAgent.SetDestination(position);
    }

    public void Stop()
    {
        m_NavMeshAgent.isStopped = true;
    }
}

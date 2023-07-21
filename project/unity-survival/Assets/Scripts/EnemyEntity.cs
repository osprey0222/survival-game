using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEntity : BaseEntity
{
    NavMeshAgent m_NavMeshAgent;
    protected override void Awake()
    {
        Health = 50f;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        if (!IsDead && GameData.Singleton.IsPlay)
        {
            if (!m_IsDamaged)
            {
                m_NavMeshAgent.isStopped = false;
                m_NavMeshAgent.SetDestination(GameManager.Singleton.Player.transform.position);
            }
            else
            {
                m_NavMeshAgent.isStopped = true;
            }
        }
        else
        {
            m_NavMeshAgent.isStopped = true;
        }
        if (m_LastPos != transform.position)
        {
            m_LastPos = transform.position;
            m_IsStationary = false;
        }
        else
        {
            m_IsStationary = true;
        }
    }
}

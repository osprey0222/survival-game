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
            if (!m_IsDeadAni)
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
        UpdateAniState();
        CheckDead();
    }

    public override void Damaged()
    {
        base.Damaged();
        Health -= GameData.Singleton.EnemyDamage;
    }
}

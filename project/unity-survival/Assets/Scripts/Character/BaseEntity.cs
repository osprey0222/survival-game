using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public enum ChState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling,
        Dead,
        Attack,
        Damage,
        Crouching,
        Leaning,
        Sliding,
    };

    // Angular speed in radians per sec.
    public float m_Speed = 3.0f;
    public float m_StationaryTolerance = 0.005f;
    public Animator m_ChaAnimator;

    protected ChState m_CurState;
    protected Vector3 m_TargetPos;
    protected CharacterController m_ChaController;
    protected Rigidbody m_RigidBody;

    [Header("Gravity")]
    [SerializeField]
    float m_Gravity = 9.8f;
    [SerializeField]
    float m_GravityMultipler = 2;
    [SerializeField]
    float m_GroundedGravity = -0.5f;
    [SerializeField]
    float m_JumpHeight = 3f;
    float m_VelocityY;
    protected Vector3 m_LastPos;
    protected bool m_IsStationary;
    ChState m_CurAniState = ChState.Idle;
    private float m_Health;

    public float Health
    {
        get
        {
            return m_Health;
        }
        set
        {
            m_Health = value;
        }
    }
    protected bool IsStationary
    {
        get
        {
            return m_IsStationary;
        }
    }
    private bool m_IsDead;
    protected bool IsDead
    {
        get
        {
            return m_IsDead;
        }
    }

    protected virtual void Awake()
    {
        m_ChaController = GetComponent<CharacterController>();
        m_RigidBody = GetComponentInChildren<Rigidbody>();
    }

    protected virtual void Start()
    {
        m_LastPos = transform.position;
    }

    protected virtual void Update()
    {
        Vector3 pos = transform.position;
        if (!m_IsDead)
        {
            Move();
            Jump();
        }

        if (m_LastPos != pos)
        {
            m_LastPos = pos;
            m_IsStationary = false;
        }
        else
        {
            m_IsStationary = true;
        }
    }

    protected virtual void LateUpdate()
    {
        if (!m_IsDead)
        {
            UpdateAniState();
        }
    }

    //protected virtual void FixedUpdate()
    //{
    //    float travelSquared = (transform.position - m_LastPos).sqrMagnitude;
    //    m_IsStationary = travelSquared < m_StationaryTolerance * m_StationaryTolerance;
    //    m_LastPos = transform.position;
    //}

    protected virtual void Move()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = m_TargetPos - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = m_Speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    protected virtual void Jump()
    {
        if (m_ChaController.isGrounded && m_VelocityY < 0f)
            m_VelocityY = m_GroundedGravity;
        if (m_ChaController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_VelocityY = Mathf.Sqrt(m_JumpHeight * 2f * m_Gravity);
        }
        m_VelocityY -= m_Gravity * m_GravityMultipler * Time.deltaTime;
        m_ChaController.Move(Vector3.up * m_VelocityY * Time.deltaTime);
    }

    protected virtual void UpdateAniState()
    {
        if (IsStationary)
        {
            ChangeState(ChState.Idle);
        }
        else
        {
            ChangeState(ChState.Running);
        }
        if (m_ChaController)
        {
            if (m_ChaController.velocity.y > 0.1f)
            {
                ChangeState(ChState.Jumping);
            }
            else if (m_ChaController.velocity.y < -0.1f)
            {
                ChangeState(ChState.Falling);
            }
        }
    }
    private void ChangeState(ChState state)
    {
        if (m_CurAniState != state)
        {
            if (m_IsDamaged)
            {
                return;
            }

            m_CurAniState = state;
            Debug.Log(m_CurAniState.ToString());
            m_ChaAnimator.Play(m_CurAniState.ToString());
        }
    }
    protected bool m_IsDamaged;
    private IEnumerator Stop()
    {
        float animationTime = 1f;
        ChangeState(ChState.Damage);
        while (animationTime > 0f)
        {
            if (m_IsDead)
            {
                m_IsDamaged = false;
                yield break;
            }
            m_IsDamaged = true;
            animationTime -= Time.deltaTime;

            yield return null;
        }
        m_IsDamaged = false;
    }

    public virtual void Damaged(float damage)
    {
        Health -= GameData.Singleton.EnemyDamage;

        if (m_Health < 0f)
        {
            m_IsDead = true;
            Dead();
            return;
        }
        if (m_CurAniState != ChState.Damage)
        {
            StartCoroutine(Stop());
        }
    }

    protected virtual void Dead()
    {
        m_ChaAnimator.Play(ChState.Dead.ToString());
        StartCoroutine(WaitDestroy());
        //damaged audio play
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}

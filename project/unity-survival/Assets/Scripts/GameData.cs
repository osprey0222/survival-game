using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Singleton { get; internal set; }
    public Action<int> OnCookieCountChanged;
    public Action OnSuccessEnd;
    public Action OnDead;
    public Action<int> OnTimeChange;
    public bool IsSuccess = false;

    private bool m_GameRunning = true;
    private float m_Time;
    [SerializeField]
    private float m_PlayerHealth;
    private int m_AttackDamage = 10;

    public float PlayerHealth
    {
        get
        {
            return m_PlayerHealth;
        }
        set
        {
            m_PlayerHealth = value;
        }
    }
    private bool m_IsDead = false;
    public bool IsDead
    {
        get
        {
            return m_IsDead;
        }
        set
        {
            if (m_IsDead != value)
            {
                m_IsDead = value;
                if (m_IsDead && OnDead != null)
                {
                    OnDead();
                }
            }
        }
    }

    internal void Damaged()
    {
        m_PlayerHealth -= m_AttackDamage;

    }

    //private int m_CurLevel = 0;
    //public int CurLevel
    //{
    //    get
    //    {
    //        return m_CurLevel;
    //    }
    //    set
    //    {
    //        m_CurLevel = value;
    //    }
    //}

    internal void ResetData()
    {
        m_IsDead = false;
        m_Time = 0;
    }

    public bool IsPlay
    {
        get
        {
            return m_GameRunning;
        }
        set
        {
            m_GameRunning = value;
            if (value)
            {
                StartCoroutine(StopWatch());
            }
        }
    }

    public bool OpenLevelUIInStartScene { get; internal set; }
    public Vector3 PlayerSpawnPos { get; internal set; }
    public float Time
    {
        get
        {
            return m_Time;
        }
    }
    public float EnemyDamage { get; internal set; }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        EnemyDamage=10f;
    }

    private IEnumerator StopWatch()
    {
        m_Time = 0f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(Config.TIMEER_INTERVAL);
            m_Time += 1f;
            if (OnTimeChange != null)
            {
                OnTimeChange((int)m_Time);
            }
            if (!m_GameRunning)
            {
                yield break;
            }
        }
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

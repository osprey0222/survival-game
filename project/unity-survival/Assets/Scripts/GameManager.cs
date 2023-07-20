using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
    [SerializeField]
    private GameObject m_Player;
    public GameObject Player
    {
        get
        {
            return m_Player;
        }
    }

    public void TakeDamage()
    {
        GameData.Singleton.Damaged();

        //m_Player.GetComponent<SoldierMovement>().Damaged();
        //(UIGame.Singleton as UIGame).Damaged();
        //if (GameData.Singleton.PlayerHealth <= 0)
        //{
        //    GameData.Singleton.IsPlay = false;
        //    m_Player.GetComponent<SoldierMovement>().Dead();
        //}
    }
}

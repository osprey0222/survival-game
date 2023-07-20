using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	EnemyHealth enemyHealth;
	NavMeshAgent nav;


	void Awake ()
	{
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> (); 
		//Debug.Log("nav " + nav);
	}

	void Update ()
	{
		if(enemyHealth.currentHealth > 0 && GameData.Singleton.PlayerHealth > 0)
		{
		//Debug.Log("nav.isOnNavMesh " + nav.isOnNavMesh);
		//Debug.Log("nav.destination " + nav.destination);
			nav.SetDestination (GameManager.Singleton.Player.transform.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
}

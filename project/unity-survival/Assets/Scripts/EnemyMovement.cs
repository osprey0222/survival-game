using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;
	SoldierHealth playerHealth;
	EnemyHealth enemyHealth;
	NavMeshAgent nav;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Debug.Log("player " + player);
		playerHealth = player.GetComponent <SoldierHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> (); 
		Debug.Log("nav " + nav);
	}


	void Update ()
	{
		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		{
		Debug.Log("nav.isOnNavMesh " + nav.isOnNavMesh);
		Debug.Log("nav.destination " + nav.destination);
			nav.SetDestination (player.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
}

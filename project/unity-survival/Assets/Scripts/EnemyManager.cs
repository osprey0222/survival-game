using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		if(!GameData.Singleton.IsPlay)
		{
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}

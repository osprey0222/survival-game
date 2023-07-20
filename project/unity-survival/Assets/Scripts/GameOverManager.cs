using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public float restartDelay = 10f;

	Animator anim;
	float restartTimer;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		//if (playerHealth.currentHealth <= 0)
		//{
		//	anim.SetTrigger("GameOver");

		//	restartTimer += Time.deltaTime;

		//	if (restartTimer >= restartDelay)
		//	{
		//		Application.LoadLevel(Application.loadedLevel);
		//	}
		//}
	}
}

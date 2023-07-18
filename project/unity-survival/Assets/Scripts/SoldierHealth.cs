using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class SoldierHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;
	AudioSource playerAudio;
	SoldierMovement playerMovement;
	SoldierShooting playerShooting;
	bool isDead;
	bool damaged;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <SoldierMovement> ();
		playerShooting = GetComponentInChildren <SoldierShooting> ();
		currentHealth = startingHealth;
	}


	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		playerAudio.Play ();

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}


	void Death ()
	{
		isDead = true;

		playerShooting.DisableEffects ();

		anim.SetTrigger ("Die");

		playerAudio.clip = deathClip;
		playerAudio.Play ();

		playerMovement.enabled = false;
		playerShooting.enabled = false;

		Destroy (gameObject, 2f);
	}


	public void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}

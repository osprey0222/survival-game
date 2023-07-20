using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class UIGame : UIBase
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    //AudioSource playerAudio;
    bool isDead;
    bool m_IsDisplayDmgEffect=false;

    //void Awake ()
    //{
    //	anim = GetComponent <Animator> ();
    //	//playerAudio = GetComponent <AudioSource> ();
    //}

    //void Update()
    //{
    //    if (m_IsDisplayDmgEffect)
    //    {
    //        damageImage.color = flashColour;
    //    }
    //    else
    //    {
    //        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    //    }
    //    m_IsDisplayDmgEffect = false;
    //}

    public void Damaged()
    {
        
        healthSlider.value = GameData.Singleton.PlayerHealth;
        if (!m_IsDisplayDmgEffect)
        {
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect()
    {
        m_IsDisplayDmgEffect = true;
        float elapsedTime = 0.0f;
        float totalTime = 1.5f;
        damageImage.color = flashColour;
        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, (elapsedTime / totalTime));
            yield return null;
        }
        m_IsDisplayDmgEffect = false;
    }
    //void Death ()
    //{
    //	isDead = true;

    //	anim.SetTrigger ("Die");

    //	playerAudio.clip = deathClip;
    //	playerAudio.Play ();

    //	GameData.Singleton.IsPlay = false;

    //	Destroy (gameObject, 2f);
    //}
}

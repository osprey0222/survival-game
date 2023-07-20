using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ECprojectileActor : MonoBehaviour
{

    public Transform spawnLocator;
    public Transform spawnLocatorMuzzleFlare;
    public Transform shellLocator;
    public Animator recoilAnimator;
    public Transform[] shotgunLocator;


    [System.Serializable]
    public class projectile
    {
        public string name;
        public Rigidbody bombPrefab;
        public GameObject muzzleflare;
        public float min, max;
        public bool rapidFire;
        public float rapidFireCooldown;

        public bool shotgunBehavior;
        public int shotgunPellets;
        public GameObject shellPrefab;
        public bool hasShells;
    }

    public projectile m_Bomb;
    public bool CameraShake = true;
    public float rapidFireDelay;
    public ECCameraShakeProjectile CameraShakeCaller;
    public bool firing;
    public int bombType = 0;
    public bool swarmMissileLauncher = false;
    public bool Torque = false;
    public float Tor_min, Tor_max;
    public bool MinorRotate;
    public bool MajorRotate = false;
    public float range = 100f;

    private AudioSource m_AudioSrc;
    private float firingTimer;
    private int seq = 0;
    public LayerMask m_ShootableMask;

    public void Awake()
    {
        m_AudioSrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        //Movement
        //if(Input.GetButton("Horizontal"))
        //{
        //    if (Input.GetAxis("Horizontal") < 0)
        //    {
        //        gameObject.transform.Rotate(Vector3.up, -25 * Time.deltaTime);
        //    }
        //    else
        //    {
        //        gameObject.transform.Rotate(Vector3.up, 25 * Time.deltaTime);
        //    }
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            firing = true;
            Fire();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            firing = false;
            firingTimer = 0;
        }

        if (m_Bomb.rapidFire && firing)
        {
            if (firingTimer > m_Bomb.rapidFireCooldown + rapidFireDelay)
            {
                Fire();
                firingTimer = 0;
            }
        }

        if (firing)
        {
            firingTimer += Time.deltaTime;
        }

    }

    public void Fire()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, m_ShootableMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            EnemyEntity enemy= hit.collider.GetComponent<EnemyEntity>();
            if (enemy != null)
            {
                enemy.Damaged();
                Debug.Log("pos");
                //enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }

        //if (CameraShake)
        //{
        //    CameraShakeCaller.ShakeCamera();
        //}
        Instantiate(m_Bomb.muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
        //   bombList.muzzleflare.Play();

        if (m_Bomb.hasShells)
        {
            Instantiate(m_Bomb.shellPrefab, shellLocator.position, shellLocator.rotation);
        }
        //recoilAnimator.SetTrigger("recoil_trigger");
        Vector3 relativePos = hit.point - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        Rigidbody rocketInstance;
        rocketInstance = Instantiate(m_Bomb.bombPrefab, spawnLocator.position, spawnLocator.rotation) as Rigidbody;
        // Quaternion.Euler(0,90,0)
        
        rocketInstance.AddForce(spawnLocator.forward * Random.Range(m_Bomb.min, m_Bomb.max));

        if (m_Bomb.shotgunBehavior)
        {
            for (int i = 0; i < m_Bomb.shotgunPellets; i++)
            {
                Rigidbody rocketInstanceShotgun;
                rocketInstanceShotgun = Instantiate(m_Bomb.bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;
                // Quaternion.Euler(0,90,0)
                rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(m_Bomb.min, m_Bomb.max));
            }
        }

        if (Torque)
        {
            rocketInstance.AddTorque(spawnLocator.up * Random.Range(Tor_min, Tor_max));
        }
        if (MinorRotate)
        {
            RandomizeRotation();
        }
        if (MajorRotate)
        {
            Major_RandomizeRotation();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        m_AudioSrc.Play();
       

        //if (Physics.Raycast(m_ShootRay, out m_ShootHit, range, shootableMask))
        //{
        //    EnemyHealth enemyHealth = m_ShootHit.collider.GetComponent<EnemyHealth>();
        //    if (enemyHealth != null)
        //    {
        //        //enemyHealth.TakeDamage(damagePerShot, shootHit.point);
        //    }
        //}
    }

    void RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 1, 0);
        }
        else if (seq == 1)
        {
            seq++;
            transform.Rotate(1, 1, 0);
        }
        else if (seq == 2)
        {
            seq++;
            transform.Rotate(1, -3, 0);
        }
        else if (seq == 3)
        {
            seq++;
            transform.Rotate(-2, 1, 0);
        }
        else if (seq == 4)
        {
            seq++;
            transform.Rotate(1, 1, 1);
        }
        else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(-1, -1, -1);
        }
    }

    void Major_RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 1)
        {
            seq++;
            transform.Rotate(0, -50, 0);
        }
        else if (seq == 2)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 3)
        {
            seq++;
            transform.Rotate(25, 0, 0);
        }
        else if (seq == 4)
        {
            seq++;
            transform.Rotate(-50, 0, 0);
        }
        else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(25, 0, 0);
        }
    }
}

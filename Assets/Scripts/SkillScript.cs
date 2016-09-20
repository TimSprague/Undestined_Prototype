using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillScript : MonoBehaviour {

    PlayerHealth playerHealth;
    ComboStates comboState;
   // EnemyScript enemy;

    MeshCollider Skill1;
    bool skill1Active = false;
    float skill1_currCooldown;
    float skill1_maxCooldown = 5;
    [SerializeField]
    Image skill1_UI;

    SphereCollider Skill2;
    bool skill2Active = false;
    float skill2_currCooldown;
    float skill2_maxCooldown = 7;
    [SerializeField]
    Image skill2_UI;

    bool skill3Active = false;
    float skill3_currCooldown;
    float skill3_maxCooldown = 9;
    [SerializeField]
    Image skill3_UI;

    private Collider[] hitCollider;
    public float sphereHitRadius;

    bool test = false;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("Skill1Cone"))
        {
            Skill1 = GameObject.Find("Skill1Cone").GetComponent<MeshCollider>();
        }
        if (GameObject.Find("Skill2Cone"))
        {
            Skill2 = GameObject.Find("Skill2Cone").GetComponent<SphereCollider>();
        }
        if (GameObject.Find("Player").GetComponent<PlayerHealth>())
        {
            playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        }
        if (GameObject.Find("Morfus"))
        {
            comboState = GameObject.Find("Morfus").GetComponent<ComboStates>();
        }
        //enemy = GameObject.Find("TestEnemy").GetComponent<EnemyScript>();

    }

    // Update is called once per frame
    void Update()
    {
        Attacks();
    }

    void FixedUpdate()
    {
        if (Skill1 != null)
        {
            if (Skill1.enabled == true)
                Skill1.enabled = false;
        }
        if (Skill2 != null)
        {
            if (Skill2.enabled == true)
                Skill2.enabled = false;
        }
    }

    void Attacks()
    {
        if ((Input.GetButton("Skill1") || Input.GetAxis("XBOX360_Skill1") != 0) && !skill1Active)
        {
            Skill1.enabled = true;
            skill1Active = true;
            SkillArea();
        }

        if ((Input.GetButtonDown("Skill2") || Input.GetAxis("XBOX360_Skill2") != 0) && !skill2Active)
        {
            // use number 2 skill
            Skill2.enabled = true;
            skill2Active = true;
            StunArea();
        }

        if (Input.GetButtonDown("Skill3") && !skill3Active)
        {
            // use number 3 skill
            skill3Active = true;
            double tempHealth;
            tempHealth = playerHealth.playerMaxHealth * 0.25;
            playerHealth.IncreaseHealth((int)tempHealth);
        }

        skillUpdate();

    }

    void skillUpdate()
    {
        if (skill1Active)
        {
            skill1_currCooldown += Time.deltaTime;
            skill1_UI.fillAmount = (skill1_currCooldown / skill1_maxCooldown);
            if (skill1_currCooldown >= skill1_maxCooldown)
            {
                skill1_currCooldown = 0;
                skill1Active = false;
            }
        }

        if (skill2Active)
        {
            skill2_currCooldown += Time.deltaTime;
            skill2_UI.fillAmount = (skill2_currCooldown / skill2_maxCooldown);
            if (skill2_currCooldown >= skill2_maxCooldown)
            {
                skill2_currCooldown = 0;
                skill2Active = false;
            }
        }

        if (skill3Active)
        {
            skill3_currCooldown += Time.deltaTime;
            skill3_UI.fillAmount = (skill3_currCooldown / skill3_maxCooldown);
            if (skill3_currCooldown >= skill3_maxCooldown)
            {
                skill3_currCooldown = 0;
                skill3Active = false;
            }
        }
    }

    void SkillArea()
    {
        Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;
        
        hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);

        foreach(Collider hitCol in hitCollider)
        {
            Debug.Log(hitCol.gameObject.name);
            if (hitCol.tag == "Enemy")
            {
               hitCol.gameObject.GetComponent<EnemyScript>().bleedTimer = 6;
               hitCol.gameObject.GetComponent<EnemyScript>().bleedDmg = 20;
               hitCol.gameObject.GetComponent<EnemyScript>().bleeding = true;
            }
        }
    }

    void StunArea()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius * 2);

        foreach (Collider hitCol in hitCollider)
        {
            Debug.Log(hitCol.gameObject.name);
            if (hitCol.tag == "Enemy")
            {
                hitCol.gameObject.GetComponent<EnemyScript>().stunTimer = 2;
                hitCol.gameObject.GetComponent<EnemyScript>().stunned = true;
            }
        }
    }
   //public void OnCollisionEnter(Collision col)
   // {
   //     test = true;
   //     if (col.gameObject.tag == "Enemy")
   //     {
   //         if (Input.GetButton("Skill1") || Input.GetAxis("XBOX360_Skill1") != 0)
   //         {
   //            col.gameObject.GetComponent<EnemyScript>().bleedTimer = 6;
   //            col.gameObject.GetComponent<EnemyScript>().bleedDmg = 20;
   //            col.gameObject.GetComponent<EnemyScript>().bleeding = true;
   //         }

   //         if (Input.GetButtonDown("Skill2") || Input.GetAxis("XBOX360_Skill2") != 0)
   //         {

   //             col.gameObject.GetComponent<EnemyScript>().isStunned();
   //         }
   //     }
   // }
}

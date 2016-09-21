using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MeleeZone : MonoBehaviour {
    public enum COMBOSTATE
    {
        lightAttack = 1, heavyAttack = 2

    };
    
    public bool attacking = false;
    public bool lightAtk = false;
    public bool heavyAtk = false;
    public EnemyScript enemScript;
    public MeleeAttack meleeAtk;
    public ComboStates comboStates;
    public float attackTimer;
    Transform playerTrans;
    public bool hitSomething;
    public ParticleSystem EnemyBlood;
    COMBOSTATE currState;
    Collider[] hitCollider;
    public float sphereHitRadius;
    // Use this for initialization
    void Start () {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //updateAtkTimer();
        //if (!attacking)
        //{

        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        //if (!attacking)
        //        //{
        //        //    lightAtk = true;
        //        //    attacking = true;
        //        //    heavyAtk = false;
        //        //    attackTimer = 1f;
        //        //}

        //        // New code - LC
        //        lightAtk = true;
        //        currState = COMBOSTATE.lightAttack;
        //        attacking = true;
        //        heavyAtk = false;
        //        meleeAtk.AnimateLightAtk();
        //        attackTimer = meleeAtk.comboTime;
        //        ComboUpdate();
        //    }

        //    if (Input.GetButtonDown("Fire2"))
        //    {
        //        //if (!attacking)
        //        //{
        //        //    attacking = true;
        //        //    heavyAtk = true;
        //        //    lightAtk = false;
        //        //    attackTimer = 1.5f;

        //        //}

        //        lightAtk = false;
        //        currState = COMBOSTATE.heavyAttack;
        //        attacking = true;
        //        heavyAtk = true;
        //        meleeAtk.AnimateHeavyAtk();
        //        attackTimer = meleeAtk.comboTime;
        //        ComboUpdate();
        //    }
        //}

    }
    void FixedUpdate()
    {
        //if (!attacking)
        //{

        //    if (Input.GetButton("Fire1"))
        //    {

        //        Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;

        //        hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);

        //        foreach (Collider hitCol in hitCollider)
        //        {
        //            Debug.Log(hitCol.gameObject.name);
        //            if (hitCol.tag == "Enemy")
        //            {
        //                hitSomething = true;
        //                float forceMod = 0;
        //                switch (hitCol.GetComponent<EnemyScript>().Identify)
        //                {
        //                    case 1:
        //                        {
        //                            enemScript = hitCol.GetComponent<MeleeEnemy>();
        //                            forceMod = enemScript.forceMod;
        //                            break;
        //                        }
        //                    case 2:
        //                        {
        //                            enemScript = hitCol.GetComponent<RangedEnemy>();
        //                            forceMod = enemScript.forceMod;
        //                            break;
        //                        }
        //                    case 3:
        //                        {
        //                            enemScript = hitCol.GetComponent<HeavyEnemy>();
        //                            forceMod = enemScript.forceMod;
        //                            break;
        //                        }
        //                }
        //                if (EnemyBlood)
        //                    Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
        //                Vector3 temp = playerTrans.forward;
        //                if (enemScript.knockedUp)
        //                {
        //                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 10) * forceMod, -5 * forceMod, (temp.normalized.z * 10) * forceMod));

        //                }
        //                else
        //                {
        //                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 750) * forceMod, 5 * forceMod, (temp.normalized.z * 750) * forceMod));

        //                }
        //                enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
        //                //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
        //                enemScript.pause = true;
        //                enemScript.hit = true;
        //                enemScript.pauseTimer = 1.25f;
        //                enemScript.TakeDmg(5);
        //                //comboStates.UpdateState((int)COMBOSTATE.lightAttack, enemScript, playerTrans);

        //                lightAtk = true;
        //                attacking = true;
        //                heavyAtk = false;
        //                attackTimer = 1f;
        //            }
        //        }


        //        // New code - LC
        //        //lightAtk = true;
        //        //attacking = true;
        //        //heavyAtk = false;
        //        //attackTimer = 1f;
        //    }

        //    if (Input.GetButton("Fire2"))
        //    {
        //        if (!attacking)
        //        {
        //            Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;

        //            hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);

        //            foreach (Collider hitCol in hitCollider)
        //            {
        //                Debug.Log(hitCol.gameObject.name);
        //                if (hitCol.tag == "Enemy")
        //                {
        //                    hitSomething = true;
        //                    float forceMod = 0;
        //                    switch (hitCol.GetComponent<EnemyScript>().Identify)
        //                    {
        //                        case 1:
        //                            {
        //                                enemScript = hitCol.GetComponent<MeleeEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                        case 2:
        //                            {
        //                                enemScript = hitCol.GetComponent<RangedEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                        case 3:
        //                            {
        //                                enemScript = hitCol.GetComponent<HeavyEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                    }
        //                    Vector3 temp = playerTrans.forward;
        //                    // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
        //                    enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
        //                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
        //                    enemScript.knockedUp = true;
        //                    enemScript.hit = true;
        //                    enemScript.pause = true;
        //                    enemScript.TakeDmg(10);
        //                    if (EnemyBlood)
        //                    {
        //                        Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
        //                    }
        //                    hitSomething = true;
        //                    attacking = true;
        //                    heavyAtk = true;
        //                    lightAtk = false;
        //                    attackTimer = 1f;

        //                }
        //            }
        //        }
        //    }


        //}
        //else
        //{
        //    if (hitSomething)
        //    {
        //        attackTimer -= Time.deltaTime;
        //        if (attackTimer <= 0)
        //        {
        //            Debug.Log("TEST");
        //            attacking = false;
        //            lightAtk = false;
        //            heavyAtk = false;
        //            hitSomething = false;
        //        }
        //    }

        //}

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Detected");
            enemScript = other.gameObject.GetComponent<EnemyScript>();

        }
    }
            

    public void OnTriggerExit(Collider other)
    {
        if (enemScript)
        {
            Debug.Log("Enemy Lost");
            enemScript = null;
            if (hitSomething)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    Debug.Log("TEST");
                    attacking = false;
                    lightAtk = false;
                    heavyAtk = false;
                    hitSomething = false;
                }
            }
           
        }
    }

    public void ComboUpdate()
    {
        comboStates.UpdateState((int)currState, enemScript, playerTrans);
    }

    void updateAtkTimer()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attackTimer = 0;
                lightAtk = heavyAtk = attacking = false;
            }
        }
    }
    //public void OnTriggerStay(Collider other)
    //{
    //    hitSomething = true;
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        enemScript = other.GetComponent<MeleeEnemy>();
    //        if (attacking)
    //        {

    //            if (lightAtk)
    //            {
    //                if (EnemyBlood)
    //                    Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
    //                Vector3 temp = playerTrans.forward;
    //                if (enemScript.knockedUp)
    //                {
    //                    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x * 10, -5, temp.normalized.z * 10));

    //                }
    //                else
    //                {
    //                    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x * 750, 5, temp.normalized.z * 750));

    //                }
    //                enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
    //                //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
    //                enemScript.pause = true;
    //                enemScript.hit = true;
    //                enemScript.pauseTimer = 1.25f;
    //                enemScript.TakeDmg(5);
    //                //comboStates.UpdateState((int)COMBOSTATE.lightAttack, enemScript, playerTrans);

    //            }
    //            if (heavyAtk)
    //            {
    //                Vector3 temp = playerTrans.forward;
    //                // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
    //                enemScript.rigidBody.velocity = new Vector3(0, 0, 0);

    //                enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x * 5, 1200, temp.normalized.z * 100f));
    //                enemScript.knockedUp = true;
    //                enemScript.hit = true;
    //                enemScript.pause = true;
    //                enemScript.TakeDmg(10);
    //                if (EnemyBlood)
    //                    Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);

    //            }

    //        }
    //    }
    //    else
    //    {
    //        lightAtk = heavyAtk = attacking = false;
    //    }
    //    lightAtk = heavyAtk = false;


    //}

    void EnemyPhysicsUpdate()
    {
        Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;

        hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);

        foreach (Collider hitCol in hitCollider)
        {
            Debug.Log(hitCol.gameObject.name);
            if (hitCol.tag == "Enemy")
            {
                hitSomething = true;
                float forceMod = 0;
                switch (hitCol.GetComponent<EnemyScript>().Identify)
                {
                    case 1:
                        {
                            enemScript = hitCol.GetComponent<MeleeEnemy>();
                            forceMod = enemScript.forceMod;
                            break;
                        }
                    case 2:
                        {
                            enemScript = hitCol.GetComponent<RangedEnemy>();
                            forceMod = enemScript.forceMod;
                            break;
                        }
                    case 3:
                        {
                            enemScript = hitCol.GetComponent<HeavyEnemy>();
                            forceMod = enemScript.forceMod;
                            break;
                        }
                }
                if (EnemyBlood)
                    Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
                Vector3 temp = playerTrans.forward;
                if (enemScript.knockedUp)
                {
                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 10) * forceMod, -5 * forceMod, (temp.normalized.z * 10) * forceMod));

                }
                else
                {
                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 750) * forceMod, 5 * forceMod, (temp.normalized.z * 750) * forceMod));

                }
                enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
                //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
                enemScript.pause = true;
                enemScript.hit = true;
                enemScript.pauseTimer = 1.25f;
                enemScript.TakeDmg(5);
                //comboStates.UpdateState((int)COMBOSTATE.lightAttack, enemScript, playerTrans);

                lightAtk = true;
                attacking = true;
                heavyAtk = false;
                attackTimer = 1f;
            }
        }
    }
}

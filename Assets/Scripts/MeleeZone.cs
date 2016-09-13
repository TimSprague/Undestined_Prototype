﻿using UnityEngine;
using System.Collections;

public class MeleeZone : MonoBehaviour {
    public enum COMBOSTATE
    {
        lightAttack = 1, heavyAttack = 2

    };
    
    public bool attacking = false;
    public bool lightAtk = false;
    public bool heavyAtk = false;
    public EnemyScript enemScript;
    public float attackTimer;
    Transform playerTrans;
    public bool hitSomething;
    public ParticleSystem EnemyBlood;
    // Use this for initialization
    void Start () {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        
        if (!attacking)
        {

            if (Input.GetButton("Fire1"))
            {
                if (!attacking)
                {
                    lightAtk = true;
                    attacking = true;
                    heavyAtk = false;
                    attackTimer = 1f;
                }
            }

            if (Input.GetButton("Fire2"))
            {
                if (!attacking)
                {
                    attacking = true;
                    heavyAtk = true;
                    lightAtk = false;
                    attackTimer = 1f;

                }
            }
       
            
        }else
        {
            if (hitSomething)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attacking = false;
                    lightAtk = false;
                    heavyAtk = false;
                    hitSomething = false;
                }
            }else
            {
                attacking = false;
                lightAtk = false;
                heavyAtk = false;
            }
        }
        
    }
    public void OnTriggerStay(Collider other)
    {
        hitSomething = true;
        if (other.gameObject.tag == "Enemy")
        {
            enemScript = other.GetComponent<MeleeEnemy>();
            if (attacking)
            {

                if (lightAtk)
                {
                    if (EnemyBlood)
                        Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
                    Vector3 temp = playerTrans.forward;
                    //if (enemScript.knockedUp)
                    //{
                    //    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x * 10, -5, temp.normalized.z * 10));
                    //}
                    //else
                    //{
                    //    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x * 10000, 5, temp.normalized.z * 10000));

                    //}
                    enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
                     enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
                    enemScript.TakeDmg(5);
                }
                if (heavyAtk)
                {
                    Vector3 temp = playerTrans.forward;
                   // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x*5, 1200, temp.normalized.z*7.5f));
                    enemScript.knockedUp = true;
                    enemScript.TakeDmg(10);
                    if (EnemyBlood)
                        Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);

                }

            }
        }else
        {
            lightAtk = heavyAtk = attacking = false;
        }
        lightAtk = heavyAtk = false;


    }
}

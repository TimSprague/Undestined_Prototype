using UnityEngine;
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
    // Use this for initialization
    void Start () {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
      
    }

    // Update is called once per frame
    void Update () {

        if (!attacking)
        {

            if (Input.GetButton("Fire1"))
            //if (Input.GetMouseButton(0))
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

                    Vector3 temp = playerTrans.forward;
                    enemScript.rigidBody.AddForce(new Vector3(temp.x * 250, 0, temp.z * 100));
                    enemScript.TakeDmg(5);
                    lightAtk = false;
                }
                if (heavyAtk)
                {
                    Vector3 temp = playerTrans.forward;
                   // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                    enemScript.rigidBody.AddForce(new Vector3(temp.normalized.x*5, 120, temp.normalized.z*7.5f));
                    enemScript.knockedUp = true;
                    enemScript.TakeDmg(10);

                }

            }
        }else
        {
            lightAtk = heavyAtk = attacking = false;
        }
        lightAtk = heavyAtk=attacking = false;


    }
}

﻿using UnityEngine;
using System.Collections;

public abstract class EnemyScript : MonoBehaviour {

    
   //Base Attributes
    public int health;
    public int maxHealth;
    public bool alive;
    public int attack;
    public int defense;
    public float speed;
    public Rigidbody rigidBody;
    //Nav Mesh for movement
    public float rotationSpeed;
    public Transform playerTransform;
    public PlayerHealth player;
    public Animation enemyAnim;
    [SerializeField]
    public Transform[] points;
    public int destPoint = 0;
    public bool pause;
    public float pauseTimer;
    public float Distance;
    public bool playerTarget;
    public bool canChange;
    public float changeTimer;
    //Status effects
    public float bleedTimer;
    public float stunTimer;
    public float time;
    public int bleedDmg;
    public bool bleeding;
    public bool stunned;
    public bool knockedUp;
    public float knockupTimer;
    public bool smashedDown;
    public float smashTimer;

    [SerializeField] EnemyUIController enemyUIcontrol;
	// Use this for initialization
	public virtual void Start () {
        rigidBody = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>().transform;
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyAnim = GameObject.Find("samuzai").GetComponent<Animation>();
      
        canChange = false;
        stunned = false;
        bleeding = false; 
        canChange = true;
        alive = true;
        knockedUp = false;
        pauseTimer = 0;
        //bleedTimer = 5; // Added for testing - LC
        //bleedDmg = 1; // Added for testing - LC
        destPoint = 0;
        rotationSpeed = 5f;
        health = maxHealth = 100;
        DamagePopupController.Initialize();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if(health <0)
        {
            DestroyImmediate(this.gameObject);
            
        }
        
        
        if(bleeding)
        {
            //TakeDmg(bleedDmg);  Use to calculate damage to health
            health -= bleedDmg; // Obsolete
        }
        if (!stunned&&! knockedUp)
        {
           



        }
        
        if (smashedDown)
        {
            smashTimer -= Time.deltaTime;

            if (smashTimer > 0.0f)
            {
                int x = 0;
            }
            else
            {
               
                smashedDown = false;
            }
        }
        if (pause)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer < 0.0f)
            {
               
                enemyAnim.CrossFade("Walk");
                pause = false;
                
            }
        }
        if(!canChange)
        {
            changeTimer -= Time.deltaTime;
            if(changeTimer<0)
            {
                canChange = true;
            }
        }
        isBleeding();
        isStunned();
        enemyAnim["Attack"].layer = 0;

        enemyUIcontrol.HealthUpdate(health, maxHealth);
        enemyUIcontrol.StatusUpdate();

    }
    public void FixedUpdate()
    {
        if (!knockedUp&&!stunned)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < Distance)
            {
           
            
                Vector3 direction = playerTransform.transform.position - transform.position;
                direction.Normalize();
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
                moveToTarget(playerTransform.position);
                playerTarget = true;
            }
            else
            {
                playerTarget = false;
                Vector3 direction = points[destPoint].position - transform.position;
                direction.Normalize();
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
               
                moveToTarget(points[destPoint].position);
            }
        }
        
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyAnim["Attack"].layer = 1;

            enemyAnim.Play("Attack");
            
           
            pause = true;
            pauseTimer = 2.5f;
        

        }
        if(other.gameObject.tag =="Terrain")
        {
            knockedUp = false;
            enemyAnim.CrossFade("Walk");
        }


    }
    // Function reserved to test damage to enemy
    //void OnMouseDown()
    //{
    //    TakeDmg(10);
    //}
    public void knockUp()
    {
        enemyAnim.Stop();


        knockedUp = true;
        enemyAnim.CrossFade("idle");
       // knockupTimer = 5.0f;
    }
    public void smashDown()
    {
        smashedDown = true;
        smashTimer = 5.0f;
    }
    public void isStunned()
    {
        if (stunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer < 0)
            {
                stunned = false;
            }
        }
    }
    public void isBleeding()
    {
        if (bleeding)
        {
            bleedTimer -= Time.deltaTime;
            if (bleedTimer < 0)
            {
                bleeding = false;
            }
        }
    }
    public void moveToTarget(Vector3 target)
    {
        Vector3 moveDirection = target - transform.position;
        Vector3 velocity = rigidBody.velocity;

        if(moveDirection.magnitude<4 &&!playerTarget)
        {
            destPoint = (destPoint + 1) % points.Length;
        }
        else
        {
            velocity = moveDirection.normalized * speed;
        }
        rigidBody.velocity = velocity;
    }

    // Use this function to update health
    public void TakeDmg(int dmg)
    {
        health -= dmg;
        DamagePopupController.CreateDamagePopup(dmg.ToString(), transform);
    }

    float GetBearing(Transform startTransform, Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - startTransform.position;

        float angleToTarget = Vector3.Angle(startTransform.forward, vectorToTarget);
        int direction = AngleDir(startTransform.forward, vectorToTarget, startTransform.up);

        return (direction == 1) ? 360f - angleToTarget : angleToTarget;
    }


    int AngleDir(Vector3 forwardVector, Vector3 targetDirection, Vector3 upVector)
    {
        float direction = Vector3.Dot(Vector3.Cross(forwardVector, targetDirection), upVector);

        if (direction > 0f)
        {
            return 1;
        }
        else if (direction < 0f)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    
    //public void GoToNextPoint()
    //{
    //    if (points.Length == 0)
    //        return;
    //    destPoint = (destPoint + 1) % points.Length;



    //    if (!playerTarget)
    //    {
    //        pause = true;
    //        pauseTimer = 5;
    //        changeTimer = 5.0f;

    //        enemyAnim.CrossFade("idle");

    //    }else
    //    {
    //        pause = true;
    //        pauseTimer = 1.5f;
    //        enemyAnim.CrossFade("idle");

    //    }

    //}
}

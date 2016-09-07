using UnityEngine;
using System.Collections;

public abstract class EnemyScript : MonoBehaviour {

    
   //Base Attributes
    public int health;
    public bool alive;
    public int attack;
    public int defense;
    public float speed;
    public Rigidbody rigidBody;
    //Nav Mesh for movement
   
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
        destPoint = 0;
      
        health = 100;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if(health <0)
        {
            DestroyImmediate(this.gameObject);
            
        }
        
        if (Vector3.Distance(playerTransform.position, transform.position) < Distance)
        {
        //Move to player
            playerTarget = true;

        }
        else
        {
            if (playerTarget == true)
            {
                
            }
            playerTarget = false;
        }
        if(bleeding)
        {
            //Bleed damage is setup when the enemy is hit by the player.
            health -= bleedDmg;
        }
        if (!stunned)
        {
            //if(agent.remainingDistance<0.05f&&canChange)
            //{
            //    canChange = false;
            //    GoToNextPoint();
            //}



        }
        if(knockedUp)
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

    }
    public void FixedUpdate()
    {

    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyAnim["Attack"].layer = 1;


            enemyAnim.CrossFade("Attack");
           
            pause = true;
            pauseTimer = 2.5f;
        

        }


    }
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
    public void GoToNextPoint()
    {
        if (points.Length == 0)
            return;
        destPoint = (destPoint + 1) % points.Length;

      
       
        if (!playerTarget)
        {
            pause = true;
            pauseTimer = 5;
            changeTimer = 5.0f;

            enemyAnim.CrossFade("idle");
           
        }else
        {
            pause = true;
            pauseTimer = 1.5f;
            enemyAnim.CrossFade("idle");
           
        }
      
    }
}

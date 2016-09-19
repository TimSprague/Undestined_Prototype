using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public abstract class EnemyScript : MonoBehaviour {

    
   //Base Attributes
    public int health;
    public int maxHealth;
    public bool alive;
    public int attack;
    protected Quaternion lookRotation;
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
    public float fallingSpeed;
    public int pathDest;
    public int pathCount;
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
    public float attackTimer;
    public bool canAttack;
    public bool hit;
    public List<Node> path;
    public ParticleSystem groundpound;
    public ParticleSystem PlayerBleed;
    //public ParticleSystem EnemyBlood;
    public Transform EnemyBloodLoc;
    public Transform GroundPoundLoc;
    [SerializeField] EnemyUIController enemyUIcontrol;
    public Pathfinding planRoute;
    // Enemy Counter
    public  int count=0;
    public CounterText countText;
    public float Dtime;
    public float dCheck;
    public int Identify;
    public float forceMod;
    // Use this for initialization
    public virtual void Start () {
        rigidBody = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>().transform;
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyAnim = GetComponentInChildren<Animation>();
        hit = false;
        canChange = false;
        stunned = false;
        bleeding = false; 
        canChange = true;
        alive = true;
        knockedUp = false;
        smashedDown = false;
        pauseTimer = 0;
        //bleedTimer = 5; // Added for testing - LC
        //bleedDmg = 1; // Added for testing - LC
        destPoint = 0;
        //  rotationSpeed = 5f;
        //health = maxHealth = 100;
        DamagePopupController.Initialize();
        //count = 0;
        SetCountText();
        planRoute = GameObject.Find("A $tar").GetComponent<Pathfinding>();
        planRoute.FindPath(transform.position, points[destPoint].position);
        path = planRoute.grid.path;
        pathCount = path.Count;
        pathDest = 0;
        Dtime = 0;
    }
    public void Awake()
    {
    //    planRoute = GameObject.Find("A*").GetComponent<Pathfinding>();
    //    planRoute.FindPath(transform.position, points[destPoint].position);
    //    path = planRoute.grid.path;
    //    pathDest = 0;
    }
	// Update is called once per frame
	public virtual void Update () {

        if(health <=0)
        {
            player.IncreaseHealth(10);
            countText.AddOne();
            alive = false;
            DestroyImmediate(this.gameObject);
        }

        if (alive)
        {
            if (!smashedDown)
            {
                smashTimer -= Time.deltaTime;
                if(smashTimer<=0)
                {
                    groundpound.Stop();
                }

                if (bleeding)
                {
                    TakeDmg(bleedDmg);  //Use to calculate damage to health
                    //health -= bleedDmg; // Obsolete
                }
              

                if (pause)
                {
                    pauseTimer -= Time.deltaTime;
                    if (pauseTimer < 0.0f)
                    {

                       enemyAnim.Play("Walk",PlayMode.StopAll);
                        pause = false;

                    }
                   
                }
               if(hit &&pause)
                {
                    enemyAnim.Play("idle", PlayMode.StopAll);
                    hit = false;
                }
                

            }
            isBleeding();
            isStunned();

            enemyUIcontrol.HealthUpdate(health, maxHealth);
            enemyUIcontrol.StatusUpdate();
        }
        Dtime += Time.deltaTime;
    }
    public void FixedUpdate()
    {
        if (!knockedUp&&!stunned)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < Distance)
            {
           
            
                Vector3 direction = playerTransform.transform.position - transform.position;
                direction.Normalize();
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
                moveToTarget(playerTransform.position);
                playerTarget = true;
            }
            else
            {
                playerTarget = false;
                Vector3 direction = path[pathDest].worldPosition - transform.position;
                direction.Normalize();
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
               
                moveToTarget(points[destPoint].position);
            }
        }
                   rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y-fallingSpeed, rigidBody.velocity.z);

    }
    public void OnCollisionEnter(Collision other)
    {
        if (alive)
        {
            // rigidBody.constraints = RigidbodyConstraints.FreezeRotationY;
            //if (other.gameObject.tag == "Player")
            //{
            //    enemyAnim["Attack"].layer = 1;
            //    player.DecreaseHealth(5);
            //    enemyAnim.Play("Attack");

            //    pause = true;
            //    pauseTimer = 2.5f;
            //     if (PlayerBleed)
            //    Instantiate(PlayerBleed, other.contacts[0].point, Quaternion.identity);
            //}
            if (other.gameObject.tag == "Player")
            {
               if (PlayerBleed)
                   Instantiate(PlayerBleed, other.contacts[0].point, Quaternion.identity);
            }
            if (other.gameObject.tag == "Terrain")
            {
                if(groundpound)
                {
                    if (smashedDown)
                    {
                        groundpound.Play();
                        smashTimer = 1.0f;
                        smashedDown = false;
                        //Instantiate(groundpound, GroundPoundLoc.position,Quaternion.identity);
                    }
                    
                }

                knockedUp = false;
            }
        }

    }
    // Function reserved to test damage to enemy
    //void OnMouseDown()
    //{
    //    TakeDmg(10);
    //}

    void OnMouseEnter()
    {
        GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);
    }

    public void knockUp()
    {
        enemyAnim.Stop();

        knockedUp = true;
       enemyAnim.CrossFade("idle");
        
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
    public virtual  void moveToTarget(Vector3 target)
    {
        
    }

    // Use this function to update health
    public void TakeDmg(int dmg)
    {
        health -= dmg;
        //if (EnemyBlood)
        //    EnemyBlood.Play();
        DamagePopupController.CreateDamagePopup(dmg.ToString(), transform);
    }

    void SetCountText()
    {
        //countText.text = count.ToString() + " / 10";
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

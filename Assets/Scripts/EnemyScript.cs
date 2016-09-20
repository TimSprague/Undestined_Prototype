using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public abstract class EnemyScript : MonoBehaviour
{


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
    [SerializeField]
    EnemyUIController enemyUIcontrol;
    public Pathfinding planRoute;
    // Enemy Counter
    public int count = 0;
    public CounterText countText;
    public float Dtime;
    public float dCheck;
    public int Identify;
    public float forceMod;
    //Status Effects
    public Transform statusLoc;
    public ParticleSystem bleedEffect;
    public ParticleSystem stunEffect;
    float bleedtime = 0;
    float stuntime = 0;
    bool bleedRoutineRunning = false;
    bool instOnceStun;
    bool instOnceBleed;
    // Use this for initialization
    public virtual void Start()
    {
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
        instOnceStun = true;
        instOnceBleed = true;
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
        if (!planRoute.FindPath(transform.position, points[destPoint].position))
        {
            DestroyImmediate(transform.parent.gameObject);
        }

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
    public virtual void Update()
    {

        if (alive)
        {
            if (!smashedDown)
            {
                smashTimer -= Time.deltaTime;
                if (smashTimer <= 0)
                {
                    groundpound.Stop();
                }

                if (pause)
                {
                    pauseTimer -= Time.deltaTime;
                    if (pauseTimer < 0.0f)
                    {

                        enemyAnim.Play("Walk", PlayMode.StopAll);
                        pause = false;

                    }

                }
                if (hit && pause)
                {
                    enemyAnim.Play("idle", PlayMode.StopAll);
                    hit = false;
                }

            }

            enemyUIcontrol.HealthUpdate(health, maxHealth);
            enemyUIcontrol.StatusUpdate();
        }
        Dtime += Time.deltaTime;
        Death();
    }
    public void FixedUpdate()
    {
        if (alive)
        {
            isStunned();

            if (!knockedUp && !stunned)
            {
                if (Vector3.Distance(playerTransform.position, transform.position) < Distance)
                {


                    Vector3 direction = playerTransform.transform.position - transform.position;
                    direction.Normalize();
                    lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
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
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y - fallingSpeed, rigidBody.velocity.z);

            if (bleeding && !bleedRoutineRunning)
            {
                if (alive)
                {
                    StartCoroutine(isBleeding(bleedTimer));
                    bleedRoutineRunning = true;
                }
            }

        }
        Death();

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
                if (groundpound)
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
            if (instOnceStun)
            {
                Instantiate(stunEffect, statusLoc.position, Quaternion.identity);
                instOnceStun = false;
            }
            stunTimer -= Time.deltaTime;
            if (stunTimer < 0)
            {
                stunned = false;
                instOnceStun = true;
            }
        }
    }
    public IEnumerator isBleeding(float timer)
    {

        int tempdmg = bleedDmg / (int)bleedTimer;
        int dmgCounter = 0;
        while (timer > 0)
        {

            timer -= 1;
            bleedtime += Time.deltaTime;

            if (alive)
            {

                DamagePopupController.CreateDamagePopup(tempdmg.ToString(), transform);
                TakeDmg(tempdmg);

                dmgCounter += tempdmg;
                ParticleSystem bleedtemp = null;
                if (instOnceBleed)
                {
                   bleedtemp = Instantiate(bleedEffect, statusLoc.position, Quaternion.identity) as ParticleSystem;
                   instOnceBleed = false;
                }
                bleedtemp.transform.position = statusLoc.position;
            }
            else
            {
                instOnceBleed = true;
                Debug.Log(dmgCounter);
                bleeding = false;
                bleedRoutineRunning = false;
                yield return null;

            }
            yield return new WaitForSeconds(1f);
        }
        bleedtime = 0;
        instOnceBleed = true;
        Debug.Log(dmgCounter);
        bleeding = false;
        bleedRoutineRunning = false;

        yield return null;
    }
    public virtual void moveToTarget(Vector3 target)
    {

    }

    // Use this function to update health
    public void TakeDmg(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            player.IncreaseHealth(10);
            countText.AddOne();
            alive = false;
        }
        //if (EnemyBlood)
        //    EnemyBlood.Play();
    }

    void Death()
    {
        if (!alive)
        {
            DestroyImmediate(this.gameObject);
        }
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

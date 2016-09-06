using UnityEngine;
using System.Collections;

public abstract class EnemyScript : MonoBehaviour {

    
   //Base Attributes
    public int health;
    public bool alive;
    public int attack;
    public int defense;
    public float speed;

    //Nav Mesh for movement
    public NavMeshAgent agent;
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
	// Use this for initialization
	public virtual void Start () {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>().transform;
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyAnim = GameObject.Find("samuzai").GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        canChange = false;
        stunned = false;
        bleeding = false;
        canChange = true;
        alive = true;
        pauseTimer = 0;
        destPoint = 0;
        agent.destination = points[0].position;
	}
	
	// Update is called once per frame
	public virtual void Update () {

        if (Vector3.Distance(playerTransform.position, agent.transform.position) < Distance)
        {
            agent.destination = playerTransform.position;
            playerTarget = true;

        }
        else
        {
            if (playerTarget == true)
            {
                agent.destination = points[destPoint % points.Length].position;
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
            if(agent.remainingDistance<0.05f&&canChange)
            {
                canChange = false;
                GoToNextPoint();
            }



        }
        if (pause)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer < 0.0f)
            {
                agent.Resume();
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
        enemyAnim["Attack"].layer = 1;

        player.DecreaseHealth(10);
        enemyAnim.CrossFade("Attack");
        agent.velocity = new Vector3(0, 0, 0);


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

        agent.destination = points[destPoint].position;
      
        float x = agent.remainingDistance;
        if (!playerTarget)
        {
            pause = true;
            pauseTimer = 5;
            changeTimer = 5.0f;

            enemyAnim.CrossFade("idle");
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }else
        {
            pause = true;
            pauseTimer = 1.5f;
            enemyAnim.CrossFade("idle");
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }
      
    }
}

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
        enemyAnim["Attack"].layer = 1;
        stunned = false;
        bleeding = false;
        alive = true;
        pauseTimer = 0;
        GoToNextPoint();
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
            if(agent.remainingDistance<0.75f)
            {
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
        isBleeding();
        isStunned();
    }

    public void OnCollisionEnter(Collision other)
    {
        player.DecreaseHealth(10);
        enemyAnim.CrossFade("Attack");

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
        agent.destination = points[destPoint].position;
      
        destPoint = (destPoint + 1)%points.Length;
        if (!playerTarget)
        {
            pause = true;
            pauseTimer = 5;
            enemyAnim.CrossFade("idle");
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }else
        {
            pause = true;
            pauseTimer = 0.75f;
            enemyAnim.CrossFade("idle");
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }
      
    }
}

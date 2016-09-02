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
    public Transform player;
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
        player = GameObject.Find("Player").GetComponent<Transform>().transform;
        agent = GetComponent<NavMeshAgent>();
       
        stunned = false;
        bleeding = false;
        alive = true;
        pauseTimer = 0;
        GoToNextPoint();
	}
	
	// Update is called once per frame
	public virtual void Update () {

        if (Vector3.Distance(player.position, agent.transform.position) < Distance)
        {
            agent.destination = player.position;
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
                pause = false;
            }
        }
        isBleeding();
        isStunned();
    }
    public void OnCollisionEnter(Collision other)
    {
        player.GetComponent<PlayerHealth>().DecreaseHealth(10);
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
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }else
        {
            pause = true;
            pauseTimer = 0.75f;
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }
      
    }
}

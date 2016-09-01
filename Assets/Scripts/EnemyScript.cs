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
    [SerializeField]
    public Transform[] points;
    public int destPoint = 0;

    //Status effects
    public float bleedTimer;
    public float stunTimer;
    public float time;
    public int bleedDmg;
    public bool bleeding;
    public bool stunned;
	// Use this for initialization
	public virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        stunned = false;
        bleeding = false;
        alive = true;
        GoToNextPoint();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
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
        isBleeding();
        isStunned();
    }
    public void isStunned()
    {
        stunTimer -= Time.deltaTime;
        if(stunTimer<0)
        {
            stunned = false;
        }
    }
    public void isBleeding()
    {
        bleedTimer -= Time.deltaTime;
        if(bleedTimer<0)
        {
            bleeding = false;
        }
    }
    public void GoToNextPoint()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        
        destPoint = (destPoint + 1)%points.Length;
    }
}

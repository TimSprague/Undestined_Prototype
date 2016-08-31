using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    
   //Base Attributes
    public int health;
    public bool alive;
    public int attack;
    public int defense;
    public float speed;




    //Status effects
    public float bleedTimer;
    public float stunTimer;
    public float time;
    public bool bleeding;
    public bool stunned;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
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
}

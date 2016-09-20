using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public bool reflected;
    bool alive;
    float aliveTimer;
    public ParticleSystem MageAttack;
	// Use this for initialization
	void Start () {
        aliveTimer = 2;
        reflected = false;
        alive = true;
        if(MageAttack)
        {
            MageAttack.Play(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(alive)
        {
            aliveTimer -= Time.deltaTime;
            if(aliveTimer<0)
            {
                DestroyImmediate(this.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(reflected)
        {
            if(other.gameObject.tag =="Enemy")
            {
                other.gameObject.GetComponent<EnemyScript>().TakeDmg(5);
                Destroy(this.gameObject);
            }
        }
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().DecreaseHealth(5);

            Destroy(this.gameObject);
        }
    }
}

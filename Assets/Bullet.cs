using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public bool reflected;
    bool alive;
    float aliveTimer;
    public ParticleSystem MageAttack;
    public ParticleSystem explosion;
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
                Instantiate(explosion, new Vector3(this.transform.position.x,this.transform.position.y + 1.5f,this.transform.position.z), Quaternion.identity);

                Destroy(this.gameObject);
            }
        }
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().DecreaseHealth(5);
            Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z), Quaternion.identity);


            Destroy(this.gameObject);
        }
    }
}

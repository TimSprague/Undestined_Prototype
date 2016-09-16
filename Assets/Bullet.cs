using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    bool alive;
    float aliveTimer;
	// Use this for initialization
	void Start () {
        aliveTimer = 2;
        alive = true;
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
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().DecreaseHealth(5);

            Destroy(this.gameObject);
        }
    }
}

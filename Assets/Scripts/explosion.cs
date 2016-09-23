using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
    public bool alive;
    public float aliveimer;
    public ParticleSystem mageAttack;
	// Use this for initialization
	void Start () {
        aliveimer = 2;
        alive = true;
        if(mageAttack)
        {
            mageAttack.Play(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	if(alive)
        {
            aliveimer -= Time.deltaTime;
                if(aliveimer<0)
            {
                DestroyImmediate(this.gameObject);
            }
        }
	}
}

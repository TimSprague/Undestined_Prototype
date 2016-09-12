using UnityEngine;
using System.Collections;

public class NewMeleeAttack : MonoBehaviour {

    public BoxCollider pushback;
    public BoxCollider liftoff;

	// Use this for initialization
	void Start () {
        pushback.enabled = false;
        liftoff.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void LeftMouseAttack()
    {
        
    }
}

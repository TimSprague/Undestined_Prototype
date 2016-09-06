using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;

    public Animator swordAnimation;

	// Use this for initialization
	void Start () {

        swordAnimation = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {

        
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            swordAnimation.Play("LightAttack");
        }

        if(Input.GetMouseButton(1))
        {
            swordAnimation.Play("HeavyAttack");
        }

	}
}

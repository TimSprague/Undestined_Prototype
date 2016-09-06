using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {
    public enum COMBOSTATE
    {
        lightAttack = 1, heavyAttack = 2

    };
    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;

    public ComboStates combScipt;
    public EnemyScript enemScript;
    public Animator swordAnimation;

    public bool attacking = false;
    public bool lightAtk = false;
    public bool heavyAtk = false;
	// Use this for initialization
	void Start () {
        combScipt = GetComponent<ComboStates>();
        swordAnimation = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            swordAnimation.Play("LightAttack");
            lightAtk = true;
            attacking = true;
        }

        if(Input.GetMouseButton(1))
        {
            swordAnimation.Play("HeavyAttack");
            attacking = true;
            heavyAtk = true;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag = "Enemy";
        enemScript = other.GetComponent<EnemyScript>();
        if (attacking)
        {
            if (lightAtk)
            {
                combScipt.UpdateState((int)COMBOSTATE.lightAttack,enemScript);
                enemScript.health -= 10;
                lightAtk = false;
                attacking = false;
            }
            if (heavyAtk)
            {
                combScipt.UpdateState((int)COMBOSTATE.heavyAttack,enemScript);
                enemScript.health -= 20;
                attacking = false;
                heavyAtk = false;
            }

        }
    }
    
}

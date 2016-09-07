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
    Transform playerTrans;
	// Use this for initialization
	void Start () {
        combScipt = GetComponent<ComboStates>();
        swordAnimation = GetComponent<Animator>();
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            swordAnimation.Play("LightAttack");

            lightAtk = true;
            attacking = true;
            heavyAtk = false;
        }

        if(Input.GetMouseButton(1))
        {
            swordAnimation.Play("HeavyAttack");

            attacking = true;
            heavyAtk = true;
            lightAtk = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemScript = other.GetComponent<MeleeEnemy>();
            if (attacking)
            {
                if (lightAtk)
                {

                    combScipt.UpdateState((int)COMBOSTATE.lightAttack, enemScript,playerTrans);
                    Vector3 temp = playerTrans.TransformDirection(-enemScript.transform.forward);
                    enemScript.rigidBody.AddForce(new Vector3(temp.x , 0, temp.z ) * 1500);
                    enemScript.health -= 10;

                    
                    lightAtk = false;
                    attacking = false;
                }
                if (heavyAtk)
                {
               
                    combScipt.UpdateState((int)COMBOSTATE.heavyAttack, enemScript, playerTrans);
                    
                    enemScript.health -= 20;
                    attacking = false;
                    heavyAtk = false;
                }

            }
        }else
        {
            if (lightAtk)
            {
                combScipt.UpdateState((int)COMBOSTATE.lightAttack, null, null);
            }
            else
            {
                combScipt.UpdateState((int)COMBOSTATE.heavyAttack, null, null);
            }
        }
    }
    
}

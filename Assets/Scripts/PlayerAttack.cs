using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] MeleeAttack animTriggers;
    [SerializeField] MeleeZone attacks;
    [SerializeField] ComboStates combos;
    public float attackDuration;
    public float comboDuration;
    public float airDuration;
    public int currCombo;
    public float comboCooldown = 0;
    public int attackType;
    EnemyScript other;
    Transform playerTrans;
    public float sphereHitRadius;
    CharacterController player;

    bool attacking;
    bool updatingPhysics;
    bool inAir;
    bool jumping;

	// Use this for initialization
	void Start ()
    {
        currCombo = 0;
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        inAir = false;
        player = playerTrans.gameObject.GetComponent<CharacterController>();

    }
	
    void FixedUpdate()
    {
        if(updatingPhysics)
        {
            Debug.Log("updatingEnemy");
            Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;
            Collider[] hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);
            combos.UpdateState(attackType, hitCollider);
            updatingPhysics = false;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        updateComboDuration();
        //if (other != attacks.enemScript)
        //    other = attacks.enemScript;

        if (player.isGrounded)
            inAir = false;
        else
            inAir = true;
        if (inAir && player.velocity.y < 0)
        {
            
            //Debug.Log("stopPlayer: " + player.velocity.y);
        }


        if (Input.GetButtonDown("Fire1") && attackDuration == 0)
        {
            animTriggers.AnimateLightAtk(currCombo++);
            comboDuration = 1.0f;
            attacking = true;
            updatingPhysics = true;
            attackType = 1;
            if(inAir)
            {
                airDuration = 1.0f;
                player.GetComponent<TestCamera>().airAttacking = true;

            }
        }
        else if(Input.GetButtonDown("Fire2") && attackDuration == 0)
        {
            animTriggers.AnimateHeavyAtk(currCombo++);
            comboDuration = 1.5f;
            attacking = true;
            updatingPhysics = true;
            attackType = 2;
            if(inAir)
            {
                airDuration = 1.0f;
                player.GetComponent<TestCamera>().airAttacking = true;
            }
        }
        else if(Input.GetButtonDown("Jump"))
        {
            //attackType = 3;
            //updatingPhysics = true;
            comboDuration = 1.5f;
            jumping = true;
            currCombo = 0;
            combos.ResetCurrentState();
        }

        if(currCombo == 3)
        {
            currCombo = 0;
            comboDuration = 0.1f;
            comboCooldown = 0.2f;
        }
    }
    void LateUpdate()
    {
        if(attacking)
        {
            if(!animTriggers.swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                attackDuration = animTriggers.swordAnimation.GetCurrentAnimatorStateInfo(0).length;
            if (comboCooldown != 0)
            {
                attackDuration += comboCooldown;
                comboCooldown = 0;
            }
            attacking = false;
        }
    }

    void updateComboDuration()
    {
        if (comboDuration > 0)
        {
            comboDuration -= Time.deltaTime;
            
            if (comboDuration < 0)
            {
                comboDuration = 0;
                currCombo = 0;
                combos.ResetCurrentState();
                //player.GetComponent<TestCamera>().airAttacking = false;
            }
        }

        if(attackDuration > 0)
        {
            attackDuration -= Time.deltaTime;
            if (attackDuration < 0)
            {
                attackDuration = 0;
                
            }
        }
        if(airDuration > 0)
        {
            airDuration -= Time.deltaTime;
            if (airDuration < 0)
            {
                airDuration = 0;
                player.GetComponent<TestCamera>().airAttacking = false;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] MeleeAttack animTriggers;
    [SerializeField] MeleeZone attacks;
    [SerializeField] ComboStates combos;
    public float attackDuration;
    public float comboDuration;
    public int currCombo;
    EnemyScript other;
    Transform playerTrans;
    bool attacking;

	// Use this for initialization
	void Start ()
    {
        currCombo = 0;
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        updateComboDuration();
        if (other != attacks.enemScript)
            other = attacks.enemScript;

        if (Input.GetButtonDown("Fire1") && attackDuration == 0)
        {
            animTriggers.AnimateLightAtk(currCombo++);
            comboDuration = 1.0f;
            attacking = true;
            combos.UpdateState(1, other, playerTrans);
        }
        else if(Input.GetButtonDown("Fire2") && attackDuration == 0)
        {
            animTriggers.AnimateHeavyAtk(currCombo++);
            comboDuration = 1.5f;
            attacking = true;
            combos.UpdateState(2, other, playerTrans);
        }
    }
    void LateUpdate()
    {
        if(attacking)
        {
            attackDuration = animTriggers.swordAnimation.GetCurrentAnimatorStateInfo(0).length;
            attacking = false;
        }
    }

    void updateComboDuration()
    {
        if (comboDuration > 0)
        {
            comboDuration -= Time.deltaTime;
            attackDuration -= Time.deltaTime;
            if (attackDuration < 0)
                attackDuration = 0;
            if (comboDuration < 0)
            {
                comboDuration = 0;
                currCombo = 0;
                combos.ResetCurrentState();
            }
        }
    }
}

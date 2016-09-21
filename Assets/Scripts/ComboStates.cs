using UnityEngine;
using System.Collections;

public class ComboStates : MonoBehaviour {
    /**
     * This enum keeps track of the current state by adding the value of the new input onto the back of the current state.
     * */
    public enum COMBOSTATE { light=1, heavy=2, jump=3, lightHeavy=12, lightHeavyJumpHeavy=1232, lightHeavyJumpLight=1231, lightHeavyJump = 123, lightLight=11, lightLightLight=111, lightLightHeavy = 112, heavyHeavy=22, heavyHeavyHeavy=222

    };
    public int currentState;
    public float comboTimer;
    public ParticleSystem EnemyBlood;
    Transform playerTrans;
    // Use this for initialization
    void Start () {
        currentState = 0;
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	//void Update ()
 //   {
 //       if(comboTimer > 0)
 //       {
 //           comboTimer -= Time.deltaTime;
 //           if(comboTimer < 0)
 //           {
 //               ResetCurrentState();
 //           }
 //       }
        
	//}
    public int GetCurrentState()
    {
        return currentState;
    }
    //Reset the current state after a set amount of time has occured in the playerscript.
    public void ResetCurrentState()
    {
        currentState = 0;
        comboTimer = 0;
    }
    
    /**
     * With each state, the current state is multipled by 10 in order to preserve the combo.
     * Each time a attack is entered, it is stored until a state in which a final move is executed.
     **/
    public void UpdateState(int newState, EnemyScript other,Transform player)
    {
        currentState *= 10;
        currentState += newState;
        //if (newState == (int)COMBOSTATE.light)
        //    comboTimer = 1.5f;
        //else if (newState == (int)COMBOSTATE.heavy)
        //    comboTimer = 2.0f;
        
        switch (currentState)
        {
            case (int)COMBOSTATE.light :
                {
                    //basic attack
                    Debug.Log("light");
                    if(other != null)
                    {
                        other.pause = true;
                        other.hit = true;
                        other.pauseTimer = 1.25f;
                        other.TakeDmg(5);
                    }
                }
                break;
            case (int)COMBOSTATE.lightHeavy:
                {
                    //perform smash up
                    if (other != null) {
                        ////Vector3 temp = player.TransformDirection(-player.transform.right);
                        ////Vector3 vel = other.rigidBody.velocity;
                        ////other.rigidBody.velocity = new Vector3(0,vel.y,vel.z*-.07f);
                        ////other.rigidBody.AddForce(new Vector3(0, 500, 80) );

                        ////other.knockUp();
                        //Vector3 temp = player.forward;
                        //// enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                        //other.rigidBody.velocity = new Vector3(0, 0, 0);

                        //other.rigidBody.AddForce(new Vector3(temp.normalized.x * 5, 1200, temp.normalized.z * 100f));
                        ////other.rigidBody.useGravity = false;
                        //other.knockedUp = true;
                        //other.hit = true;
                        //other.pause = true;
                        //other.TakeDmg(10);
                        //StartCoroutine(knockupenemy(other));

                    //            Vector3 playerPos = GameObject.Find("MeleeZone").transform.position;

        //            hitCollider = Physics.OverlapSphere(playerPos, sphereHitRadius);

        //            foreach (Collider hitCol in hitCollider)
        //            {
        //                Debug.Log(hitCol.gameObject.name);
        //                if (hitCol.tag == "Enemy")
        //                {
        //                    hitSomething = true;
        //                    float forceMod = 0;
        //                    switch (hitCol.GetComponent<EnemyScript>().Identify)
        //                    {
        //                        case 1:
        //                            {
        //                                enemScript = hitCol.GetComponent<MeleeEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                        case 2:
        //                            {
        //                                enemScript = hitCol.GetComponent<RangedEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                        case 3:
        //                            {
        //                                enemScript = hitCol.GetComponent<HeavyEnemy>();
        //                                forceMod = enemScript.forceMod;
        //                                break;
        //                            }
        //                    }
        //                    Vector3 temp = playerTrans.forward;
        //                    // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
        //                    enemScript.rigidBody.velocity = new Vector3(0, 0, 0);
        //                    enemScript.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
        //                    enemScript.knockedUp = true;
        //                    enemScript.hit = true;
        //                    enemScript.pause = true;
        //                    enemScript.TakeDmg(10);
        //                    if (EnemyBlood)
        //                    {
        //                        Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
        //                    }
        //                    hitSomething = true;
        //                    attacking = true;
        //                    heavyAtk = true;
        //                    lightAtk = false;
        //                    attackTimer = 1f;
                    }
                }
                break;
            case (int)COMBOSTATE.lightLight:
                {
                    //2nd basic attack
                    Debug.Log("lightlight");
                    if(other != null)
                    {
                        other.pause = true;
                        other.hit = true;
                        other.pauseTimer = 1.25f;
                        other.TakeDmg(5);
                    }
                    
                }
                break;
            case (int)COMBOSTATE.lightHeavyJumpHeavy:
                {
                    //Perfrom down smash
                    //Call function
                    if (other != null)
                    {
                        Vector3 temp = player.TransformDirection(-player.transform.right);
                        Vector3 vel = other.rigidBody.velocity;
                        other.smashedDown = true;
                        other.rigidBody.velocity = new Vector3(0, -vel.y*5, vel.z*-.1f);
                        other.rigidBody.AddForce(new Vector3(0, -250*temp.y, 1500  *temp.z ) );
                    }
                }
                break;
            case (int)COMBOSTATE.lightLightLight:
                {
                    //flurry attack
                    //CallFunction
                    Debug.Log("lightlightlight");
                    if (other != null)
                    {
                        Vector3 temp = player.forward;
                        if (other.knockedUp)
                        {
                            other.rigidBody.AddForce(new Vector3(temp.normalized.x * 10, -5, temp.normalized.z * 10));

                        }
                        else
                        {
                            other.rigidBody.AddForce(new Vector3(temp.normalized.x * 750, 5, temp.normalized.z * 750));

                        }
                        other.rigidBody.velocity = new Vector3(0, 0, 0);
                        //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
                        other.pause = true;
                        other.hit = true;
                        other.pauseTimer = 1.25f;
                        other.TakeDmg(5);
                    }
                    ResetCurrentState();
                }
                break;
            case (int)COMBOSTATE.lightLightHeavy:
                {
                    /**double slash with knock back
                    Call function
                       **/
                    if (other != null)
                    {
                        Vector3 temp = player.TransformDirection(-other.transform.forward);
                        Vector3 vel = other.rigidBody.velocity;
                        other.rigidBody.velocity = new Vector3(0, vel.y, vel.z * -25);
                        other.rigidBody.AddForce(new Vector3(0, 150,16000*temp.z));
                    }
                }
                break;
            case (int)COMBOSTATE.lightHeavyJump:
                {
                    //Intermediate step
                    
                }
                break;
            case (int)COMBOSTATE.heavy:
                {
                    Debug.Log("heavy");
                    if (other != null)
                    {
                        other.pause = true;
                        other.hit = true;
                        other.pauseTimer = 1.25f;
                        other.TakeDmg(10);
                    }
                }
                break;
            case (int)COMBOSTATE.heavyHeavy:
                {
                    Debug.Log("heavyHeavy");
                    if (other != null)
                    {
                        other.pause = true;
                        other.hit = true;
                        other.pauseTimer = 1.25f;
                        other.TakeDmg(10);
                    }
                }
                break;
            case (int)COMBOSTATE.heavyHeavyHeavy:
                {
                    Debug.Log("heavyHeavyHeavy");
                    if (other != null)
                    {
                        Vector3 temp = player.forward;
                        // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                        other.rigidBody.velocity = new Vector3(0, 0, 0);

                        other.rigidBody.AddForce(new Vector3(temp.normalized.x * 5, 1200, temp.normalized.z * 100f));
                        other.knockedUp = true;
                        other.hit = true;
                        other.pause = true;
                        other.TakeDmg(10);
                        //if (EnemyBlood)
                        //    Instantiate(EnemyBlood, enemScript.EnemyBloodLoc.position, GetComponent<Transform>().rotation);

                    }
                    ResetCurrentState();
                }
                break;

        }
    }

    public void UpdateState(int newState, Collider[] enemiesHit, bool air = false)
    {
        currentState *= 10;
        currentState += newState;

        switch(currentState)
        {
            case (int)COMBOSTATE.light:
                {
                    // basic attack
                    Debug.Log("light");
                    foreach(Collider hitCol in enemiesHit)
                    {
                        if(hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            EnemyStatusUpdate(enemy, 5);
                        }
                    }
                    
                }
                break;
            case (int)COMBOSTATE.lightLight:
                {
                    Debug.Log("lightLight");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            EnemyStatusUpdate(enemy, 5);
                        }
                    }
                }
                break;
            case (int)COMBOSTATE.lightLightLight:
                {
                    Debug.Log("lightLightLight");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            
                            Vector3 temp = playerTrans.forward;
                            if (enemy.knockedUp)
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 10) * forceMod, -5 * forceMod, (temp.normalized.z * 10) * forceMod));

                            }
                            else
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 750) * forceMod, 5 * forceMod, (temp.normalized.z * 750) * forceMod));

                            }
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
                            
                            EnemyStatusUpdate(enemy, 5);
                        }
                    }
                }
                break;
            case (int)COMBOSTATE.heavy:
                {
                    Debug.Log("heavy");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            Vector3 temp = playerTrans.forward;
                            // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            if (enemy.knockedUp)
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, -1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                                enemy.knockedUp = false;
                                knockdownenemy(enemy);
                            }
                            //else
                            //{
                            //    
                            //        enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                            //        enemy.knockedUp = true;
                            //        StartCoroutine(knockupenemy(enemy));
                            //    
                            //}

                            EnemyStatusUpdate(enemy, 10);
                        }
                    }
                }
                break;
            case (int)COMBOSTATE.heavyHeavy:
                {
                    Debug.Log("heavyHeavy");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            EnemyStatusUpdate(enemy, 10);
                        }
                    }
                }
                break;

            case (int)COMBOSTATE.heavyHeavyHeavy:
                {
                    Debug.Log("heavyHeavy");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            Vector3 temp = playerTrans.forward;
                            // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                            enemy.knockedUp = true;
                            EnemyStatusUpdate(enemy, 10);
                        }
                    }
                }
                break;
            case (int)COMBOSTATE.lightHeavy:
                {
                    // knockup
                    Debug.Log("lightHeavy");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>(); 
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            Vector3 temp = playerTrans.forward;
                            // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            if(enemy.knockedUp)
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, -1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                                enemy.knockedUp = false;
                                knockdownenemy(enemy);

                            }
                            else
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                                enemy.knockedUp = true;
                                StartCoroutine(knockupenemy(enemy));
                            }

                            EnemyStatusUpdate(enemy, 10);
                            
                        }
                    }
                }
                break;

            case (int)COMBOSTATE.lightLightHeavy:
                {
                    // knockup
                    Debug.Log("lightLightHeavy");
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            Vector3 temp = playerTrans.forward;
                            // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            if (enemy.knockedUp)
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, -1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                                enemy.knockedUp = false;
                                knockdownenemy(enemy);

                            }
                            else
                            {
                                enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, 1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                                enemy.knockedUp = true;
                                StartCoroutine(knockupenemy(enemy));
                            }

                            EnemyStatusUpdate(enemy, 10);

                        }
                    }
                }
                break;

            case (int)COMBOSTATE.lightHeavyJump:
                {
                    // knockup
                    Debug.Log("lightHeavyJump");
                }
                break;
            case (int)COMBOSTATE.lightHeavyJumpHeavy:
                {
                    // aerial knockdown
                    Debug.Log("lightHeavyJumpHeavy");
                    //playerTrans.gameObject.GetComponent<CharacterController>().Move(Vector3.zero);
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;
                            Vector3 temp = playerTrans.forward;
                            // enemScript.rigidBody.velocity = new Vector3(temp.normalized.x*5, temp.normalized.y, temp.normalized.z*5);
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            knockdownenemy(enemy);
                            enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 5) * forceMod, -1200 * forceMod, (temp.normalized.z * 100f) * forceMod));
                            enemy.knockedUp = false;
                            EnemyStatusUpdate(enemy, 10);
                            enemy.pauseTimer += 2.5f;
                            
                        }
                    }
                    
                }
                break;
            case (int)COMBOSTATE.lightHeavyJumpLight:
                {
                    // aerial knockdown
                    Debug.Log("lightHeavyJumpHeavy");
                    //playerTrans.gameObject.GetComponent<CharacterController>().Move(Vector3.zero);
                    foreach (Collider hitCol in enemiesHit)
                    {
                        if (hitCol.tag == "Enemy")
                        {
                            EnemyScript enemy = null;
                            float forceMod = 0;
                            switch (hitCol.GetComponent<EnemyScript>().Identify)
                            {
                                case 1:
                                    {
                                        enemy = hitCol.GetComponent<MeleeEnemy>();
                                        break;
                                    }
                                case 2:
                                    {
                                        enemy = hitCol.GetComponent<RangedEnemy>();
                                        break;
                                    }
                                case 3:
                                    {
                                        enemy = hitCol.GetComponent<HeavyEnemy>();
                                        break;
                                    }
                            }
                            forceMod = enemy.forceMod;

                            Vector3 temp = playerTrans.forward;
                            enemy.rigidBody.constraints &= ~RigidbodyConstraints.FreezePosition;
                            enemy.rigidBody.AddForce(new Vector3((temp.normalized.x * 1000) * forceMod, -5 * forceMod, (temp.normalized.z * 1000) * forceMod));

                            
                            enemy.rigidBody.velocity = new Vector3(0, 0, 0);
                            //   enemScript.rigidBody.velocity = new Vector3(enemScript.rigidBody.velocity.x, enemScript.rigidBody.velocity.y, enemScript.rigidBody.velocity.z+75);
                            
                            EnemyStatusUpdate(enemy, 5);

                        }
                    }

                }
                break;
        }
    }
    IEnumerator knockupenemy(EnemyScript other)
    {
        yield return new WaitForSeconds(0.5f);
        if(other)
        other.rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        //yield return new WaitForSeconds(1.0f);
        //other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        //other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        //other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        //other.rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    IEnumerator unfreezePlayerAir()
    {
        yield return new WaitForSeconds(0.5f);
        playerTrans.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
    }
    void knockdownenemy(EnemyScript other)
    {
        other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        other.rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        other.rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    void EnemyStatusUpdate(EnemyScript currEnemy, int dmg)
    {
        currEnemy.hit = true;
        currEnemy.pause = true;
        currEnemy.pauseTimer = 2f;
        currEnemy.TakeDmg(dmg);
        if (EnemyBlood)
        {
            Instantiate(EnemyBlood, currEnemy.EnemyBloodLoc.position, GetComponent<Transform>().rotation);
        }
    }
}

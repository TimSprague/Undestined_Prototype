using UnityEngine;
using System.Collections;

public class ComboStates : MonoBehaviour {
    /**
     * This enum keeps track of the current state by adding the value of the new input onto the back of the current state.
     * */
    public enum COMBOSTATE { light=1, heavy=2, jump=3, lightHeavy=12, lightHeavyJumpHeavy=1232, lightHeavyJump = 123, lightLight=11, lightLightLight=111, lightLightHeavy = 112

    };
    public int currentState;
    public float comboTimer;
	// Use this for initialization
	void Start () {
        currentState = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if(comboTimer < 0)
            {
                ResetCurrentState();
            }
        }
        
	}
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
        comboTimer = 1.0f;
        switch (currentState)
        {
            case (int)COMBOSTATE.light :
                {
                    //basic attack
                    Debug.Log("light");

                }
                break;
            case (int)COMBOSTATE.lightHeavy:
                {
                    //perform smash up
                    if (other != null) {
                        //Vector3 temp = player.TransformDirection(-player.transform.right);
                        Vector3 vel = other.rigidBody.velocity;
                        other.rigidBody.velocity = new Vector3(0,vel.y,vel.z*-.07f);
                        other.rigidBody.AddForce(new Vector3(0, 500, 80) );

                        other.knockUp();
                    }
                }
                break;
            case (int)COMBOSTATE.lightLight:
                {
                    //2nd basic attack
                    Debug.Log("lightlight");
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

        }
    }
}

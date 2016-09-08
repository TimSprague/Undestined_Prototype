using UnityEngine;
using System.Collections;

public class ComboStates : MonoBehaviour {
    /**
     * This enum keeps track of the current state by adding the value of the new input onto the back of the current state.
     * */
    public enum COMBOSTATE { light=1 , lightHeavy=12, lightHeavyJumpHeavy=1232, lightHeavyJump = 123, lightLight=11, lightLightLight=111, lightLightHeavy = 112

    };
    public int currentState;
    public float comboTimer;
	// Use this for initialization
	void Start () {
        currentState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        comboTimer -= Time.deltaTime;
        if(comboTimer<0.0f)
        {
            ResetCurrentState();
            
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

                    
                }
                break;
            case (int)COMBOSTATE.lightHeavy:
                {
                    //perform smash up
                    if (other != null) {
                        Vector3 temp = player.TransformDirection(-player.transform.right);
                        Vector3 vel = other.rigidBody.velocity;
                        other.rigidBody.velocity = new Vector3(0,vel.y,vel.z*-.07f);
                        other.rigidBody.AddForce(new Vector3(0, 5000, 80) );

                        other.knockUp();
                    }
                }
                break;
            case (int)COMBOSTATE.lightLight:
                {
                    //2nd basic attack
                   
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
                        other.rigidBody.velocity = new Vector3(0, -vel.y*5, vel.z*-.1f);
                        other.rigidBody.AddForce(new Vector3(0, -700*temp.y, 1000  *temp.z ) );
                    }
                }
                break;
            case (int)COMBOSTATE.lightLightLight:
                {
                    //flurry attack
                    //CallFunction
                    
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
                        other.rigidBody.AddForce(new Vector3(0, 100, 8000));
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

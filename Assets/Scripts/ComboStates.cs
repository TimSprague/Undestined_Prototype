using UnityEngine;
using System.Collections;

public class ComboStates : MonoBehaviour {
    /**
     * This enum keeps track of the current state by adding the value of the new input onto the back of the current state.
     * */
    public enum COMBOSTATE { light=1 , lightHeavy=12, lightHeavyJumpHeavy=1232, lightHeavyJump = 123, lightLight=11, lightLightLight=111, lightLightHeavy = 112

    };
    private int currentState;
	// Use this for initialization
	void Start () {
        currentState = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
        
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
    public void UpdateState(int newState)
    {
        switch(currentState)
        {
            case (int)COMBOSTATE.light :
                {
                    //basic attack
                    currentState *= 10;
                    currentState += newState;

                }
                break;
            case (int)COMBOSTATE.lightHeavy:
                {
                    //perform smash up
                    currentState *= 10;
                    currentState += newState;
                }
                break;
            case (int)COMBOSTATE.lightLight:
                {
                    //2nd basic attack
                    currentState *= 10;
                    currentState += newState;
                }
                break;
            case (int)COMBOSTATE.lightHeavyJumpHeavy:
                {
                    //Perfrom down smash
                    //Call function
                    currentState = 0;
                }
                break;
            case (int)COMBOSTATE.lightLightLight:
                {
                    //flurry attack
                    //CallFunction
                    currentState = 0;
                }
                break;
            case (int)COMBOSTATE.lightLightHeavy:
                {
                    //double slash with knock back
                    //Call function
                    currentState = 0;
                }
                break;
            case (int)COMBOSTATE.lightHeavyJump:
                {
                    //Intermediate step
                    currentState *= 10;
                    currentState += newState;
                }
                break;

        }
    }
}

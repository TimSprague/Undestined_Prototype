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
    public Animator swordAnimation;

	// Use this for initialization
	void Start () {
        combScipt = GetComponent<ComboStates>();
        swordAnimation = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            swordAnimation.Play("LightAttack");
            combScipt.UpdateState((int)COMBOSTATE.lightAttack);
        }

        if(Input.GetMouseButton(1))
        {
            swordAnimation.Play("HeavyAttack");
            combScipt.UpdateState((int)COMBOSTATE.heavyAttack);

        }

    }

    public IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForFixedUpdate();
    }
}

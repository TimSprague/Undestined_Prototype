using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //float v = vSpeed * Input.GetAxis("Mouse X");
        //transform.Translate(0, 0, 0);

        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0, 0, -90 * Time.deltaTime);
            //transform.Translate(Vector3.left * Time.deltaTime * swordTurn);
        }

        if(Input.GetMouseButton(1))
        {
            transform.Rotate(90 * Time.deltaTime, 0, 0);
            //transform.Translate(Vector3.right * Time.deltaTime * swordTurn);
        }

	}
}

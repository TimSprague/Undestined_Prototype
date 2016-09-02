using UnityEngine;
using System.Collections;

public class Raycast3 : MonoBehaviour {
    public float distance3 = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            distance3 = hit.distance;
        }
	}
}

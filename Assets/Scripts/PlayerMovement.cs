using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    NavMeshAgent nav;
    Transform playerTransform;
	// Use this for initialization
	void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
        playerTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
        
	}
}

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public Transform cameraTransform;
    public float forwardVel;
    public float rotateVel;
    Quaternion targetRotation;
    Rigidbody rb;
    public float jumpHeight;
    public bool jumping;
    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }
	// Use this for initialization
	void Start () {

        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        jumping = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Turn();
	}

    void FixedUpdate()
    {
        Move();
    }

    void Turn()
    {
        // multiplying rotation is adding to previous rotation
        targetRotation *= Quaternion.AngleAxis(rotateVel * Input.GetAxis("Horizontal") * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    void Move()
    {

        rb.velocity = transform.forward * Input.GetAxis("Vertical") * forwardVel;

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            rb.velocity = new Vector3(0, jumpHeight, 0);
            jumping = true;
        }

        rb.velocity += (0.5f * Physics.gravity);
        
    }

    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Terrain")
        {
        }
            jumping = false;
    }
}

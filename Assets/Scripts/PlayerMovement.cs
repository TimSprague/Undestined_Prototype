using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Transform playerTransform;
    Rigidbody rb;

    public float speed;
    public float turnSpeed;

    bool jumping = false;
    public int jumpHeight = 8;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        playerTransform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, jumpHeight, 0);
        }

    }

    
}

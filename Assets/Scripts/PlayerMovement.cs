using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{

    public Transform cameraTransform;
    Rigidbody rb;

    public float speed;
    public float turnSpeed;

    bool jumping = false;
    public int jumpHeight = 8;

    CharacterController cc;
    Quaternion targRotation;
    float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targRotation; }
    }
    // Use this for initialization
    void Start()
    {
        // playerTransform = GetComponent<Transform>();
        // rb.isKinematic = false;
        cc = GetComponent<CharacterController>();

        targRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        else
            Debug.LogError("Character needs RigidBody");
        forwardInput = turnInput = 0;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Turn()
    {
        // multiplying adds to the last roation
        targRotation *= Quaternion.AngleAxis(turnSpeed * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targRotation;
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //rb.velocity = cameraTransform.forward * forwardInput * speed;

        Vector3 moveForward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * speed;
        // rotate
        // transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0));
        cc.Move(moveForward * Time.deltaTime);

        //playerTransform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, jumpHeight, 0);
        }

    }


}

﻿using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {


    public float moveSpeed  = 10; // how fast the player moves
    public float jumpSpeed = 10; // how high the player jumps
    private Vector3 moveDirection = Vector3.zero; // which direction is the character moving
    private Animation anim; // the animation component
    private CharacterController character; // A reference to the ThirdPersonCharacter on the object

    public float gravity = 50; // how hard gravity pulls down
    public bool airAttacking = false;


    public float lookSpeed = 5; // mouse look sensitivity
    public float tiltUpRange = 20; // tilt up angle of the camera
    public float tiltDownRange = 10; // tilt down angle of the camera
    public int zoomOut = 75; // zoom out fov of the camera
    public int zoomIn = 45; // zoom in fov of the camera
    public int zoomSpeed = 3; // the speed of teh camera zoom in/out
    private float yRot; // rotation value of camera
    private float xRot; // rotation value of camera

    Transform target;
    public Animator PlayerAnimator;
    private float runSpeed = 0;
    private float straifeSpeed = 0;

    private float slowMove = 1.0f;
	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        character = GetComponent<CharacterController>();
        target = GameObject.Find("CameraTarget").GetComponent<Transform>();
        //anim = GetComponent<Animation>();
        //anim.Play("Idle");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //mouse
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            CameraRotate();
        }
        //player movement
        if (character.isGrounded)
        {
           
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= (moveSpeed * slowMove);
            
            if (Input.GetButton("Jump"))
            {
                
                moveDirection.y = jumpSpeed;
                //PlayerAnimator.SetTrigger("JumpTrigger");

            }
        }

        // Apply gravity
        if(!airAttacking)
            moveDirection.y -= gravity * Time.deltaTime;
        else
        {
            moveDirection.x = 0;
            moveDirection.y = 0;
            moveDirection.z = 0;
        }
        // Move the player
        character.Move(moveDirection * Time.deltaTime);
        

        //bool zoom = Input.GetAxis("Zoom") > 0;
        //if (zoom)
        //{
        //    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomIn, zoomSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomOut, zoomSpeed * Time.deltaTime);
        //}
    }

    void CameraRotate()
    {
        yRot += lookSpeed * Input.GetAxis("Mouse X");
        xRot += (lookSpeed / 4) * Input.GetAxis("Mouse Y");
        xRot = Mathf.Clamp(xRot + Input.GetAxis("Mouse Y"), -tiltUpRange, tiltDownRange); // limit the camera rotation up/down
        transform.rotation = Quaternion.Euler(0, yRot, 0); // rotate the player when moving the camera Y-axis

        //if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, yRot, 0); // rotate the player when moving the camera Y-axis
        //}

        //limit the camera look up/down rotation
        Camera.main.transform.rotation = Quaternion.Euler(-xRot, yRot, 0); // rotate the camera when moving the camera

        float distance = 5;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y +5,transform.position.z), transform.TransformDirection(Vector3.forward), out hit))
        {
            distance = hit.distance;
        }

        if (distance > 8)
        {
            distance = 8;
        }

        Vector3 dir = new Vector3(0.0f, 0.0f, -distance);

        //Camera.main.transform.position = transform.position + Camera.main.transform.rotation * dir;
        //Camera.main.transform.LookAt(target.position);


    }

    void LateUpdate()
    {
        if (PlayerAnimator)
        {
            PlayerAnimator.SetBool("Moving", false);
            PlayerAnimator.SetBool("Strafing", false);
            if(Input.GetAxis("Roll")>0)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    if (Input.GetAxis("Vertical") > 0)
                        PlayerAnimator.Play("Unarmed-Roll-Forward");
                    else
                        PlayerAnimator.Play("Unarmed-Roll-Backward");
                    return;
                }
                if (Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetAxis("Horizontal") > 0)
                        PlayerAnimator.Play("Unarmed-Roll-Right");
                    else
                        PlayerAnimator.Play("Unarmed-Roll-Left");
                    return;
                }
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                runSpeed += Input.GetAxis("Vertical") > 0 ? 0.04f : -0.04f;
                if (runSpeed < 0)
                    SlowMoveSpeed(0.5f);
                else
                    SlowMoveSpeed(1.0f);
                PlayerAnimator.SetBool("Moving", true);
            }
            else
                runSpeed = 0;
            if (Input.GetAxis("Horizontal") != 0)
            {
                straifeSpeed += Input.GetAxis("Horizontal") > 0 ? 0.04f : -0.04f;
                PlayerAnimator.SetBool("Moving", true);
                PlayerAnimator.SetBool("Strafing", true);
            }
            else
                straifeSpeed = 0;
            PlayerAnimator.SetFloat("Velocity X", straifeSpeed);
            PlayerAnimator.SetFloat("Velocity Z", runSpeed);

            if (Input.GetButtonDown("Jump"))
            {
                //PlayerAnimator.SetInteger("Jumping", 1);
                //PlayerAnimator.SetTrigger("JumpTrigger");
            }

        }
    }
    public void SlowMoveSpeed(float input)
    {
        slowMove = input;
    }
    public void PlayPlayerFall()
    {
        if (PlayerAnimator)
        {
            //PlayerAnimator.SetInteger("Jumping", 0);
            //PlayerAnimator.SetTrigger("JumpTrigger");
        }
    }
}

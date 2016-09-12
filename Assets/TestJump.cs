using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TestJump : MonoBehaviour {

    public float jumpPower;
    public float moveSpeed;
    Rigidbody rb;
    public bool jumping;
    bool test2Jump;
    public float tempYPos;
    public float jumpHeight;
    ComboStates comboState;
    public float jumpSpeed;
    PlayerHealth playerHealth;



    MeshCollider Skill1;
    bool skill1Active = false;
    float skill1_currCooldown;
    float skill1_maxCooldown = 5;
    [SerializeField]
    Image skill1_UI;

    SphereCollider Skill2;
    bool skill2Active = false;
    float skill2_currCooldown;
    float skill2_maxCooldown = 7;
    [SerializeField]
    Image skill2_UI;

    bool skill3Active = false;
    float skill3_currCooldown;
    float skill3_maxCooldown = 9;
    [SerializeField]
    Image skill3_UI;

    [SerializeField]
    Transform camTransform;
    [SerializeField]
    Transform targetTranform;

    Quaternion lastForward;


    // Use this for initialization
    void Start () {

        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }

        jumping = false;

        if (GameObject.Find("Skill1Cone"))
        {
            Skill1 = GameObject.Find("Skill1Cone").GetComponent<MeshCollider>();
        }
        if (GameObject.Find("Skill2Cone"))
        {
            Skill2 = GameObject.Find("Skill2Cone").GetComponent<SphereCollider>();
        }
        if (GameObject.Find("Player").GetComponent<PlayerHealth>())
        {
            playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        }
        if (GameObject.Find("Morfus"))
        {
            comboState = GameObject.Find("Morfus").GetComponent<ComboStates>();
        }
        lastForward = transform.rotation;
    }
	
    void Update()
    {
        Attacks();
    }
    // Update is called once per frame
    void FixedUpdate () {

        Move();

        if (Skill1 != null)
        {
            if (Skill1.enabled == true)
                Skill1.enabled = false;
        }
        if (Skill2 != null)
        {
            if (Skill2.enabled == true)
                Skill2.enabled = false;
        }
    }

    void Attacks()
    {
        if ((Input.GetButton("Skill1") || Input.GetAxis("XBOX360_Skill1") != 0) && !skill1Active)
        {
            Skill1.enabled = true;
            skill1Active = true;
        }

        if ((Input.GetButtonDown("Skill2") || Input.GetAxis("XBOX360_Skill2") != 0) && !skill2Active)
        {
            // use number 2 skill
            Skill2.enabled = true;
            skill2Active = true;

        }

        if (Input.GetButtonDown("Skill3") && !skill3Active)
        {
            // use number 3 skill
            skill3Active = true;
            double tempHealth;
            tempHealth = playerHealth.playerMaxHealth * 0.25;
            playerHealth.IncreaseHealth((int)tempHealth);
        }

        skillUpdate();
    }
  
    void Move()
    {
        //rb.velocity += camTransform.right * Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetAxis("Vertical") != 0)
        {
            //rb.velocity = new Vector3(targetTranform.position.normalized.x * moveSpeed, 0, targetTranform.position.normalized.z * moveSpeed);
            transform.Translate(-targetTranform.forward * Input.GetAxis("Vertical") * moveSpeed, Space.World);
            //rb.MovePosition(transform.position + camTransform.forward * Input.GetAxis("Vertical") * moveSpeed);
        }
        //rb.velocity += camTransform.forward * Input.GetAxis("Vertical") * moveSpeed;
        //rb.velocity += transform.right * Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(camTransform.right * Input.GetAxis("Horizontal") * moveSpeed, Space.World);

            //rb.MovePosition(transform.position + transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        }


        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //Quaternion tempQuat = transform.rotation;
            //if (camTransform.rotation.y > 0)
            //    tempQuat.y = camTransform.rotation.y - 360;
            //else
            //    tempQuat.y = camTransform.rotation.y;
            //transform.rotation = tempQuat;
            //lastForward = tempQuat;
            Vector3 direction = targetTranform.position - transform.position;
            direction.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * 5);
           
        }
       

        //rb.velocity += transform.forward * Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetButton("Jump") && !jumping)
        {
            JumpRoutine(1.0f);
            //rb.velocity += new Vector3(0,jumpPower,0);
            //GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
        }
    }

    void JumpRoutine(float timer)
    {
        float temp = 0;
        while (temp < timer)
        {
            temp += Time.deltaTime;

            jumping = true;

            if (!test2Jump)
            {
                tempYPos = rb.position.y + jumpHeight;
                test2Jump = true;
            }

            if (jumping)
            {
                if (rb.position.y >= (tempYPos - .25f))
                {
                    test2Jump = false;
                    tempYPos -= jumpHeight;
                }
            }

            if (test2Jump)
            {
                rb.position = Vector3.MoveTowards(rb.position, new Vector3(rb.position.x, tempYPos, rb.position.z), jumpSpeed);
            }

        }
        if (comboState != null)
        {
            comboState.UpdateState(3, null, null);
        }
    }

    void skillUpdate()
    {
        if (skill1Active)
        {
            skill1_currCooldown += Time.deltaTime;
            skill1_UI.fillAmount = (skill1_currCooldown / skill1_maxCooldown);
            if (skill1_currCooldown >= skill1_maxCooldown)
            {
                skill1_currCooldown = 0;
                skill1Active = false;
            }
        }

        if (skill2Active)
        {
            skill2_currCooldown += Time.deltaTime;
            skill2_UI.fillAmount = (skill2_currCooldown / skill2_maxCooldown);
            if (skill2_currCooldown >= skill2_maxCooldown)
            {
                skill2_currCooldown = 0;
                skill2Active = false;
            }
        }

        if (skill3Active)
        {
            skill3_currCooldown += Time.deltaTime;
            skill3_UI.fillAmount = (skill3_currCooldown / skill3_maxCooldown);
            if (skill3_currCooldown >= skill3_maxCooldown)
            {
                skill3_currCooldown = 0;
                skill3Active = false;
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Terrain")
        {
            jumping = false;
        }
    }
}

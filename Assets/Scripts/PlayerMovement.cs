using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public Transform cameraTransform;
    public float forwardVel;
    public float rotateVel;
    Quaternion targetRotation;
    Rigidbody rb;
    public float jumpHeight;
    public bool jumping;
    public float fallingSpeed;

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

    PlayerHealth playerHealth;
    ComboStates comboState;
    GameObject currentEnemy;
    float UI_timer = 0;
    public float UI_fadeInOutSpeed = 2;


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

    }

    // Update is called once per frame
    void Update ()
    {
        Turn();
        Attacks();
	}

    void FixedUpdate()
    {
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

    void Turn()
    {
        // multiplying rotation is adding to previous rotation
        targetRotation *= Quaternion.AngleAxis(rotateVel * Input.GetAxis("Horizontal") * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    void Move()
    {

        rb.velocity = transform.forward * Input.GetAxis("Vertical") * forwardVel;
        

        if (Input.GetButton("Jump") && !jumping)
        {
            //rb.velocity = (new Vector3(0, jumpHeight, 0));
            StartCoroutine(JumpRoutine(0.5f));
        }

        rb.velocity += (new Vector3(rb.velocity.x, rb.velocity.y - fallingSpeed, rb.velocity.z));

    }

    IEnumerator JumpRoutine(float timer)
    {
        float temp = 0;
        while (temp < timer)
        {
            rb.velocity += (new Vector3(0, jumpHeight, 0));
            temp += Time.deltaTime;
        }
        jumping = true;
        if (comboState != null)
        {
            comboState.UpdateState(3, null, null);
        }
        yield return null;
       
    }

    void Attacks()
    {
        if ((Input.GetButton("Skill1") || Input.GetAxis("XBOX360_Skill1") !=0) && !skill1Active)
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

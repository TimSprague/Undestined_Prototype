using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        Transform myTransform;
        MeshCollider Skill1;
        SphereCollider Skill2;
        PlayerHealth playerHealth;
        ComboStates comboState;
        GameObject currentEnemy;
        float UI_timer = 0;
        public float UI_fadeInOutSpeed = 2;

        bool skill1Active = false;
        float skill1_currCooldown;
        float skill1_maxCooldown = 5;
        [SerializeField] Image skill1_UI;

        bool skill2Active = false;
        float skill2_currCooldown;
        float skill2_maxCooldown = 7;
        [SerializeField] Image skill2_UI;

        bool skill3Active = false;
        float skill3_currCooldown;
        float skill3_maxCooldown = 9;
        [SerializeField] Image skill3_UI;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            Skill1 = GameObject.Find("Skill1Cone").GetComponent<MeshCollider>();
            Skill2 = GameObject.Find("Skill2Cone").GetComponent<SphereCollider>();
            playerHealth = GetComponent<PlayerHealth>();
            comboState = GameObject.Find("Morfus").GetComponent<ComboStates>();
            myTransform = transform;
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                //if (m_Jump)
                //{
                //    comboState.UpdateState(3, null, null);
                //}
            }

            if (Input.GetMouseButton(0))
            {
                // melee light 
            }
            if (Input.GetMouseButton(1))
            {
                // heavy melee
            }
            if (Input.GetButtonDown("Skill1") || Input.GetAxis("XBOX360_Skill1") != 0)
            {
                // use the number 1 skill BLEED in forward cone range 5 yards
                Skill1.enabled = true;
                skill1Active = true;
            }
            if (Input.GetButtonDown("Skill2") || Input.GetAxis("XBOX360_Skill2") != 0)
            {
                // use number 2 skill
                Skill2.enabled = true;
                skill2Active = true;
            }
            if (Input.GetButtonDown("Skill3"))
            {
                // use number 3 skill
                skill3Active = true;
                double tempHealth;
                tempHealth = playerHealth.playerMaxHealth * 0.25;
                playerHealth.IncreaseHealth((int)tempHealth);
            }

            // Display Enemy Info if reticule intersects with enemy - LC
            CheckEnemyInfo();

            skill1Update();
            skill2Update();
            skill3Update();
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift))
                m_Move *= 567486578.0f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);

            if (m_Jump && m_Character.m_IsGrounded)
                m_Jump = false;

            if (Skill1.enabled == true)
                Skill1.enabled = false;

            if (Skill2.enabled == true)
                Skill2.enabled = false;
        }

        void skill1Update()
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
        }

        void skill2Update()
        {
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
        }

        void skill3Update()
        {
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

        void CheckEnemyInfo()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Enemy" && Vector3.Distance(hit.transform.position, myTransform.position) < 250.0f)
            {
                Debug.Log("Found a target");
                hit.transform.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
                currentEnemy = hit.transform.gameObject;

                Color imageC = currentEnemy.GetComponentInChildren<Image>().color;
                Color textC = currentEnemy.GetComponentInChildren<Text>().color;
                UI_timer += Time.deltaTime;
                imageC.a = Mathf.Lerp(0, 1, UI_timer * UI_fadeInOutSpeed);
                textC.a = Mathf.Lerp(0, 1, UI_timer * UI_fadeInOutSpeed);

                currentEnemy.GetComponentInChildren<Image>().color = imageC;
                currentEnemy.GetComponentInChildren<Text>().color = textC;
            }
            else
            {
                if (currentEnemy != null)
                    currentEnemy.transform.gameObject.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
                currentEnemy = null;
                UI_timer = 0;
            }
        }
    }
}

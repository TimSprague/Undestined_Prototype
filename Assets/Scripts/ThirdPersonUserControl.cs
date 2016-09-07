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
        MeshCollider Skill1;
        SphereCollider Skill2;
        PlayerHealth playerHealth;

        GameObject currentEnemy;
        float UI_timer = 0;
        public float UI_fadeInOutSpeed = 2;

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

        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (Input.GetMouseButton(0))
            {
                // melee light 
            }
            if (Input.GetMouseButton(1))
            {
                // heavy melee
            }
            if (Input.GetButtonDown("Skill1"))
            {
                // use the number 1 skill BLEED in forward cone range 5 yards
                Skill1.enabled = true;
            }
            if (Input.GetButtonDown("Skill2"))
            {
                // use number 2 skill
                Skill2.enabled = true;
            }
            if (Input.GetButtonDown("Skill3"))
            {
                // use number 3 skill
                double tempHealth;
                tempHealth = playerHealth.playerMaxHealth * 0.25;
                playerHealth.IncreaseHealth((int)tempHealth);
            }

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Enemy")
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
    }
}
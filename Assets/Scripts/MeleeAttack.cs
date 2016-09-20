using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour {
   
    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;
    [SerializeField] float comboTime = 0.0f;
    [SerializeField] int currentCombo = 0;
    public Transform player;
    public Animator swordAnimation;
    public Animator playerAnimation;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] List<AudioClip> soundLightSwordSwings;

    [SerializeField] ParticleSystem particle_groundPound;
    [SerializeField] Transform transform_groundPound;
    Transform playerTrans;
    //private TestCamera testCamera;
	// Use this for initialization
	void Start () {
        swordAnimation = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (GameObject.Find("Sound Source"))
            sfxSource = GameObject.Find("Sound Source").GetComponent<AudioSource>();
        soundLightSwordSwings.Add(Resources.Load<AudioClip>("Audio/lightSword1"));
        soundLightSwordSwings.Add(Resources.Load<AudioClip>("Audio/heavySword1"));
       // testCamera = GetComponentInParent<TestCamera>();
	}
	
	// Update is called once per frame
	void Update () {

        if(comboTime > 0)
        {
            comboTime -= Time.deltaTime;
            if (comboTime <= 0)
            {
                comboTime = 0;
                currentCombo = 0;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentCombo == 0 || comboTime == 0)
            {
                if (swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                {
                    swordAnimation.Play("LightAttack");
                    currentCombo++;
                }

            }
            else if (currentCombo == 1 && comboTime > 0)
            {
                if (!swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("firstAttack"))
                {
                    swordAnimation.Play("LightAttack2");
                    currentCombo++;

                }

                //        swordAnimation.Play("LightAttack2");
                //        currentCombo++;

                //}
                //else if (currentCombo == 2 && comboTime > 0)
                //{

                //        currentCombo = 0;
                //        swordAnimation.Play("LightAttack3");

                //}
                //comboTime = 1.0f;

                swordAnimation.Play("LightAttack");
                //playerAnimation.Play("Unarmed-Attack-R3");
            }
            else if (currentCombo == 2 && comboTime > 0)
            {
                if (!swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("secondAttack"))
                {
                    currentCombo = 0;
                    swordAnimation.Play("LightAttack3");
                }
            }
            comboTime = 1.0f;
            /// COMMENT THIS OUT IF USING CONDITION STATEMENTS ABOVE
            //swordAnimation.Play("LightAttack");
            playerAnimation.Play("Unarmed-Attack-R3");  // UNCOMMENT BACK
        }
        if(Input.GetButtonDown("Fire2"))
        {
            if (currentCombo == 0 || comboTime == 0)
            {
                if (!swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("thirdAttack"))
                {
            swordAnimation.Play("HeavyAttack");
                    currentCombo++;
        }
            }
            else if (currentCombo == 1 && comboTime > 0)
            {
                if (!swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("firstAttack"))
                {
                    swordAnimation.Play("HeavyAttack2");
                    currentCombo++;
                }
            }
            else if (currentCombo == 2 && comboTime > 0)
            {
                if (!swordAnimation.GetCurrentAnimatorStateInfo(0).IsTag("secondAttack"))
                {
                    currentCombo = 0;
                    swordAnimation.Play("HeavyAttack3");
                }
            }
            comboTime = 1.5f;
            /// COMMENT THIS OUT IF USING CONDITION STATEMENTS ABOVE
            //swordAnimation.Play("HeavyAttack");
            playerAnimation.Play("Unarmed-Attack-Kick-L1"); // UNCOMMENT BACK
        }
    }
    public void FixedUpdate()
    {
        

    }


    #region Unity Call Functions
    void lightSwordSwing()
    {
        AudioClip clip = null;
        float maxVol = sfxSource.volume;

        if (soundLightSwordSwings.Count > 0)
            clip = soundLightSwordSwings[0];
        maxVol = UnityEngine.Random.Range(0.2f, 0.5f);

        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, maxVol);
        }
    }

    void heavySwordSwing()
    {
        AudioClip clip = null;
        float maxVol = sfxSource.volume;

        if (soundLightSwordSwings.Count > 0)
            clip = soundLightSwordSwings[1];
        maxVol = UnityEngine.Random.Range(0.2f, 0.5f);

        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, maxVol);
        }
    }

    void swordPound_Particle()
    {
        if(particle_groundPound)
        {
            ParticleSystem temp = (ParticleSystem)Instantiate(particle_groundPound, transform_groundPound.position, particle_groundPound.gameObject.transform.rotation);

        }
    }

    #endregion
}

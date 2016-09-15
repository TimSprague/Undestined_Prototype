using UnityEngine;
using System.Collections;

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
    [SerializeField] AudioClip[] soundLightSwordSwings;

    [SerializeField] ParticleSystem particle_groundPound;
    [SerializeField] Transform transform_groundPound;
    Transform playerTrans;

	// Use this for initialization
	void Start () {
        swordAnimation = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        //if(comboTime > 0)
        //{
        //    comboTime -= Time.deltaTime;
        //    if (comboTime <= 0)
        //    {
        //        comboTime = 0;
        //        currentCombo = 0;
        //    }
        //}
        if (Input.GetButtonDown("Fire1"))
        {
            //if (currentCombo == 0 || comboTime == 0)
            //{
                
            //        swordAnimation.Play("LightAttack");
            //        currentCombo++;
                
            //}
            //else if (currentCombo == 1 && comboTime > 0)
            //{
                
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
            playerAnimation.Play("Unarmed-Attack-R3");
        }

        if(Input.GetButtonDown("Fire2"))
        {
            //if (currentCombo == 0 || comboTime == 0)
            //{

            //    swordAnimation.Play("HeavyAttack");
            //    currentCombo++;

            //}
            //else if (currentCombo == 1 && comboTime > 0)
            //{

            //    swordAnimation.Play("HeavyAttack2");
            //    currentCombo++;

            //}
            //else if (currentCombo == 2 && comboTime > 0)
            //{

            //    currentCombo = 0;
            //    swordAnimation.Play("HeavyAttack3");

            //}
            //comboTime = 1.0f;
            swordAnimation.Play("HeavyAttack");
            playerAnimation.Play("Unarmed-Attack-Kick-L1");
        }

    }
    public void FixedUpdate()
    {
        

    }


    void swordSwing()
    {
        AudioClip clip = null;
        float maxVol = sfxSource.volume;

        if (soundLightSwordSwings.GetLength(0) > 0)
            clip = soundLightSwordSwings[0];
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
            Instantiate(particle_groundPound, transform_groundPound.position, particle_groundPound.gameObject.transform.rotation);

        }
    }
}

using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {
    public enum COMBOSTATE
    {
        lightAttack = 1, heavyAttack = 2

    };
    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;

    public ComboStates combScipt;
    public EnemyScript enemScript;
    public Animator swordAnimation;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] soundLightSwordSwings;
     
    public bool attacking = false;
    public bool lightAtk = false;
    public bool heavyAtk = false;
    Transform playerTrans;

	// Use this for initialization
	void Start () {
        combScipt = GetComponent<ComboStates>();
        swordAnimation = GetComponent<Animator>();
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            swordAnimation.Play("LightAttack");
            lightAtk = true;
            attacking = true;
            heavyAtk = false;
        }

        if(Input.GetButton("Fire2"))
        {
            swordAnimation.Play("HeavyAttack");
            attacking = true;
            heavyAtk = true;
            lightAtk = false;
        }

        

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemScript = other.GetComponent<MeleeEnemy>();
            if (attacking)
            {
                if (lightAtk)
                {

                    combScipt.UpdateState((int)COMBOSTATE.lightAttack, enemScript,playerTrans);
                    //Vector3 temp = playerTrans.TransformDirection(-enemScript.transform.right);
                    ////Vector3 vel = enemScript.rigidBody.velocity;
                    ////enemScript.rigidBody.velocity = new Vector3(0, vel.y, vel.z * -25);
                    //enemScript.rigidBody.AddForce(new Vector3(temp.x * 35, 0, temp.z * 35) );
                    // enemScript.health -= 10;
                    enemScript.TakeDmg(10);
                    other.GetComponent<MeleeEnemy>().PlayBleedParticle();

                    lightAtk = false;
                    heavyAtk = false;
                    attacking = false;
                }
                if (heavyAtk)
                {
               
                    combScipt.UpdateState((int)COMBOSTATE.heavyAttack, enemScript, playerTrans);
                    other.GetComponent<MeleeEnemy>().PlayBleedParticle();

                    enemScript.TakeDmg(20);
                    attacking = false;
                    lightAtk = false;
                    heavyAtk = false;
                }

            }
        }
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
}

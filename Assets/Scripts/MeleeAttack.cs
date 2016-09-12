using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {
   
    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;

    public Animator swordAnimation;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] soundLightSwordSwings;
     
   
    Transform playerTrans;

	// Use this for initialization
	void Start () {
        swordAnimation = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            swordAnimation.Play("LightAttack");
           
        }

        if(Input.GetButton("Fire2"))
        {
            swordAnimation.Play("HeavyAttack");
           
        

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

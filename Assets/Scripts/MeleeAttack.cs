﻿using UnityEngine;
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
    
    // Use this for initialization
    void Start () {

    public bool attacking = false;
    public bool lightAtk = false;
    public bool heavyAtk = false;
	// Use this for initialization
	void Start () {
        combScipt = GetComponent<ComboStates>();
        swordAnimation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            swordAnimation.Play("LightAttack");
            lightAtk = true;
            attacking = true;
        }

        if(Input.GetMouseButton(1))
        {
            swordAnimation.Play("HeavyAttack");
            attacking = true;
            heavyAtk = true;
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

                    combScipt.UpdateState((int)COMBOSTATE.lightAttack, enemScript);
                    enemScript.health -= 10;

                    Vector3 temp = transform.TransformDirection(transform.forward);
                    enemScript.rigidBody.AddForce(new Vector3(temp.x,2.5f,temp.z)*100);
                   
                    lightAtk = false;
                    attacking = false;
                }
                if (heavyAtk)
                {
                    combScipt.UpdateState((int)COMBOSTATE.heavyAttack, enemScript);
                    enemScript.health -= 20;
                    attacking = false;
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

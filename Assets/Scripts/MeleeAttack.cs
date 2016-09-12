﻿using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {
   
    public float vSpeed = 2.0f;
    public float swordTurn = 2.0f;
    public float speed = 10.0f;
    [SerializeField] float comboTime = 0.0f;
    [SerializeField] int currentCombo = 0;

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
            //    if(!swordAnimation.IsInTransition(0))
            //    {
            //        swordAnimation.Play("LightAttack");
            //        currentCombo++;
            //    }
            //}
            //else if(currentCombo == 1 && comboTime > 0)
            //{
            //    if (!swordAnimation.IsInTransition(0))
            //    {
            //        swordAnimation.Play("LightAttack2");
            //        currentCombo++;
            //    }
            //}
            //else if(currentCombo == 2 && comboTime > 0)
            //{
            //    if (!swordAnimation.IsInTransition(0))
            //    {
            //        currentCombo = 0;
            //        swordAnimation.Play("LightAttack3");
            //    }
            //}
            //comboTime = 1.0f;

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

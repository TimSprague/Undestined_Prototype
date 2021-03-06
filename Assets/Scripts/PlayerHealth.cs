﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float playerCurrentHealth;
    public float playerMaxHealth;
    public bool isAlive = true;
    public Image HealthImage;
    public Image HealthBar;
    public Text DEAD;
    public AudioClip DamageClip;
    public float DamageFlashSpeed = 5f;
    public Color DamageColor = new Color(1f,0f,0f,1f);
    bool damaged;
    public AudioClip HealClip;
    public float HealFlashSpeed = 5f;
    public Color HealColor = new Color(0f, 1f, 0f, 1f);
    bool healed;
    public AudioSource audioSource;
    public ParticleSystem PlayerBleed;
    private Animator playerAnimator;
    public LevelManager reload;
    GameObject instance;
    // Use this for initialization
    void Start () {
        playerAnimator = GetComponent<Animator>();
        audioSource = GameObject.Find("Sound Source").GetComponent<AudioSource>();
        DamageClip = Resources.Load<AudioClip>("Audio/Player_Pain");
        
	}
	
	// Update is called once per frame
	void Update () {

        //playerCurrentHealth -= 10;
        //HealthBarUpdate();
        if (playerCurrentHealth <= 0)
        {
            Death();
        }
        if (HealthImage)
        {
            //Flash Screen Wnen damaged
            if (damaged)
                HealthImage.color = DamageColor;
            else
                HealthImage.color = Color.Lerp(HealthImage.color, Color.clear, DamageFlashSpeed * Time.deltaTime);
            damaged = false;
            //Flash Screen when healed
            if (healed)
                HealthImage.color = HealColor;
            else
                HealthImage.color = Color.Lerp(HealthImage.color, Color.clear, HealFlashSpeed * Time.deltaTime);
            healed = false;
        }

    }

   public void DecreaseHealth(float _value)
    {
        if (PlayerBleed && !PlayerBleed.isPlaying)
        {
            PlayerBleed.Play();
        }

        if (isAlive)
        {
            damaged = true;
            if(DamageClip && audioSource)
                audioSource.PlayOneShot(DamageClip);    
            playerCurrentHealth -= _value;
            HealthBarUpdate();
        }
        if (playerAnimator)
            playerAnimator.Play("Unarmed-GetHit-F1");
        if (playerCurrentHealth <= 0)
        {
            Death();
        }
    }

   public void IncreaseHealth(int _value)
    {
        if (isAlive)
        {
            if(HealClip && audioSource)
                audioSource.PlayOneShot(HealClip);
            healed = true;
            if (playerCurrentHealth < playerMaxHealth)
                playerCurrentHealth += _value;

            if (playerCurrentHealth > playerMaxHealth)
                playerCurrentHealth = playerMaxHealth;

            HealthBarUpdate();
        }
    }

    public void IncreasePower(int _value)
    {

    }


    void Death()
    {
        isAlive = false;

        if (playerAnimator)
        {
            playerAnimator.Play("Unarmed-Death1");
            StartCoroutine(RestartCurrentScene(3f));
        }
        //GameObject.Destroy(gameObject);
    }
    public void DecreasePower(int _value)
    {

    }
    public void HealthBarUpdate()
    {
        if (HealthBar)
            HealthBar.fillAmount = ((float)playerCurrentHealth / (float)playerMaxHealth);
        if (DamageClip && audioSource)
        {
            audioSource.pitch = Random.Range(0.9f, 1.0f);
            audioSource.PlayOneShot(DamageClip);
        }

    }

    IEnumerator RestartCurrentScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(0);
    }
}

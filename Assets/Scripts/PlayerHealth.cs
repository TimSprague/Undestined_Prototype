using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int playerCurrentHealth;
    public int playerMaxHealth;
    public bool isAlive = true;
    public Image HealthImage;
    public Image HealthBar;
    public AudioClip DamageClip;
    public float DamageFlashSpeed = 5f;
    public Color DamageColor = new Color(1f,0f,0f,1f);
    bool damaged;
    public AudioClip HealClip;
    public float HealFlashSpeed = 5f;
    public Color HealColor = new Color(0f, 1f, 0f, 1f);
    bool healed;

    // Use this for initialization
    void Start () {
	
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

   public void DecreaseHealth(int _value)
    {
        if (isAlive)
        {
            damaged = true;
            playerCurrentHealth -= _value;
            HealthBarUpdate();
        }

        if (playerCurrentHealth <= 0)
        {
            Death();
        }
    }

   public void IncreaseHealth(int _value)
    {
        if (isAlive)
        {
            healed = true;
            playerCurrentHealth += _value;
            HealthBarUpdate();
        }
    }


    void Death()
    {
        isAlive = false;
    }

    void HealthBarUpdate()
    {
        if (HealthBar)
            HealthBar.fillAmount = ((float)playerCurrentHealth / (float)playerMaxHealth);
    }
}

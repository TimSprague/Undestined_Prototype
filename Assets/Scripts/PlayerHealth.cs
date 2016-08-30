using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int playerCurrentHealth;
    public int playerMaxHealth;
    public bool isAlive = true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DecreaseHealth(int _value)
    {
        if (isAlive)
        {
            playerCurrentHealth -= _value;
        }
        if (playerCurrentHealth <= 0)
        {
            Death();
        }
    }

    void IncreaseHealth(int _value)
    {
        if (isAlive)
        {
            playerCurrentHealth += _value;
        }
    }


    void Death()
    {
        isAlive = false;
    }
}

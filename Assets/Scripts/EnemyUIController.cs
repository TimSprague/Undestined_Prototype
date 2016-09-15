using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyUIController : MonoBehaviour {

    public Text currentStatus;
    public Image health;
    public EnemyScript enemy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void HealthUpdate(float currHealth, float maxHealth)
    {
        Vector3 scale = health.gameObject.transform.localScale;
        scale.x = (currHealth / maxHealth);
        if (scale.x < 0)
            scale.x = 0;
        health.gameObject.transform.localScale = scale;
    }

    public void StatusUpdate()
    {
        if (enemy.bleeding)
        {
            if (currentStatus.text != "Bleeding")
            {
                currentStatus.text = "Bleeding";
                currentStatus.color = new Color(255, 0, 0);
            }
        }
        else
        {
            currentStatus.text = null;
            currentStatus.color = new Color(0, 0, 0);
        }

        if (enemy.stunned)
        {
            if (currentStatus.text != "Stunned")
            {
                currentStatus.text = "Stunned";
                currentStatus.color = new Color(255, 255, 0);
            }
        }
        else
        {
            currentStatus.text = null;
            currentStatus.color = new Color(0, 0, 0);
        }
        
    }
}

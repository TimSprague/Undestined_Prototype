using UnityEngine;
using System.Collections;

public class DamagePopupTest : MonoBehaviour {

    public int health;
    public int maxHealth;
    [SerializeField]
    EnemyUIController enemyUIcontrol;
    // Use this for initialization
    void Start ()
    {
        DamagePopupController.Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseDown()
    {
        TakeDmg(10);
    }

    public void TakeDmg(int dmg)
    {
        health -= dmg;
        DamagePopupController.CreateDamagePopup(dmg.ToString(), transform);
    }
}

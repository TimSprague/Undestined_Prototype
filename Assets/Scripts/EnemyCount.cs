using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyCount : MonoBehaviour {

    // Enemy Counter
    private int count;
    public Text countText;
    Vector3 spawnLocation;
    bool enemy;

    // Use this for initialization
    void Start () {
        count = 0;
        enemy = GameObject.Find("Enemy").GetComponent<EnemyScript>().alive;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemy == false)
        {
            count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString() + " / 10";
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public GameObject terrain;

    public float spawntime;
    public float spawningtime;
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start ()
    {
        //enemy = GameObject.Find("Enemy");
        terrain = GameObject.Find("Terrain");
        InvokeRepeating("Spawn", spawntime, spawntime);
        
    }

    void Update()
    {
        spawningtime += Time.deltaTime;

        if(spawningtime >= 20.0f)
        {
            CancelInvoke("Spawn");
        }
    }

    void Spawn ()
    {
	    if(playerHealth.playerCurrentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject temp = (GameObject)Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        Transform[] terr_transform = new Transform[18];

        terr_transform[0] = terrain.transform.GetChild(1).GetChild(0).GetComponent<Transform>(); // Lanterns_post 
        terr_transform[1] = terrain.transform.GetChild(1).GetChild(1).GetComponent<Transform>(); // Lanterns_post (1)
        terr_transform[2] = terrain.transform.GetChild(1).GetChild(2).GetComponent<Transform>(); // Lanterns_post (2)
        terr_transform[3] = terrain.transform.GetChild(1).GetChild(3).GetComponent<Transform>(); // Lanterns_post (3)
        terr_transform[4] = terrain.transform.GetChild(1).GetChild(4).GetComponent<Transform>(); // Lanterns_post (4)
        terr_transform[5] = terrain.transform.GetChild(1).GetChild(5).GetComponent<Transform>(); // Lanterns_post (5)
        terr_transform[6] = terrain.transform.GetChild(1).GetChild(6).GetComponent<Transform>(); // Lanterns_post (6)
        terr_transform[7] = terrain.transform.GetChild(0).GetChild(0).GetComponent<Transform>(); // Hay_b 
        terr_transform[8] = terrain.transform.GetChild(0).GetChild(1).GetComponent<Transform>(); // Hay_b (1)
        terr_transform[9] = terrain.transform.GetChild(0).GetChild(2).GetComponent<Transform>(); // Hay_b (2)
        terr_transform[10] = terrain.transform.GetChild(0).GetChild(3).GetComponent<Transform>(); // Hay_b (3)
        terr_transform[11] = terrain.transform.GetChild(0).GetChild(4).GetComponent<Transform>(); // Hay_b (4)
        terr_transform[12] = terrain.transform.GetChild(0).GetChild(5).GetComponent<Transform>(); // Hay_b (5)
        terr_transform[13] = terrain.transform.GetChild(0).GetChild(6).GetComponent<Transform>(); // Hay_b (6)
        terr_transform[14] = terrain.transform.GetChild(0).GetChild(7).GetComponent<Transform>(); // Hay_b (7)
        terr_transform[15] = terrain.transform.GetChild(0).GetChild(8).GetComponent<Transform>(); // Hay_b (8)
        terr_transform[16] = terrain.transform.GetChild(0).GetChild(9).GetComponent<Transform>(); // Hay_d
        terr_transform[17] = terrain.transform.GetChild(0).GetChild(10).GetComponent<Transform>(); // Hay_d (1)

        int terr_point1 = Random.Range(0, 17);
        int terr_point2 = Random.Range(0, 17);

        if(terr_point1 != terr_point2)
        {
            temp.GetComponent<EnemyScript>().points[0] = terr_transform[terr_point1];
            temp.GetComponent<EnemyScript>().points[1] = terr_transform[terr_point2];
        }
        else
        {
            while (terr_point1 == terr_point2)
            {
                terr_point2 = Random.Range(0, 17);
            }
        }

    }
}

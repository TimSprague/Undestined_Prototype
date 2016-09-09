using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public GameObject terrain;

    public float spawntime;
    public Transform[] spawnPoints;
    public Transform[] patrol;
    Transform patrolpoint;

	// Use this for initialization
	void Start ()
    {
        enemy = GameObject.Find("Enemy");
        terrain = GameObject.Find("Terrain");
        InvokeRepeating("Spawn", spawntime, spawntime);
        patrol = new Transform[5];

        patrol[0] = terrain.transform.GetChild(5).GetChild(0).GetComponent<Transform>();
        patrol[1] = terrain.transform.GetChild(4).GetChild(1).GetComponent<Transform>();
    }
	
	void Spawn ()
    {
	    if(playerHealth.playerCurrentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        

        GameObject temp = (GameObject)Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        temp.GetComponent<EnemyScript>().points[0] = patrol[0];
        temp.GetComponent<EnemyScript>().points[1] = patrol[1];
    }
}

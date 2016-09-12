using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {
    public float DestroyTime = 1.0f;
    float timer = 0;
    bool start = false;
	// Use this for initialization
	void Start () {
        start = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
            timer += Time.deltaTime;
        if (timer >= DestroyTime)
            Destroy(gameObject);
	}
}

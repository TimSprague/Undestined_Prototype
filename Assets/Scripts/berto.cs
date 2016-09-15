using UnityEngine;
using System.Collections;

public class berto : MonoBehaviour {

    AudioClip theRealMusic;
    AudioClip bertoMusic;
	// Use this for initialization
	void Start ()
    {
        theRealMusic = Resources.Load<AudioClip>("Audio/Fighting-Words_60_WSR2442501");
        bertoMusic = Resources.Load<AudioClip>("Audio/Cinco-Hombres_FULL_WSR1211101");
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().clip = theRealMusic;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().clip = bertoMusic;
            gameObject.GetComponent<AudioSource>().Play();
        }
	}
}

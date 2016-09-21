using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public PlayerHealth playerhealth;
    public Animator playerAnimator;

	// Use this for initialization
	void Start () {
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerhealth.playerCurrentHealth <= 0)
        {
            
            //SceneManager.LoadScene("Prototype 2");
            restartCurrentScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public IEnumerator restartCurrentScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }


}

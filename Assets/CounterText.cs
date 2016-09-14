using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CounterText : MonoBehaviour {
    private int count;
    private Text text;
	// Use this for initialization
	void Start () {
        count = 0;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = count.ToString() + " / 35";
    }
    public void AddOne()
    {
        count += 1;
    }
}

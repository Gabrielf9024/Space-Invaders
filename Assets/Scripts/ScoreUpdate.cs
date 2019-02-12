using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {

    // Use this for initialization
    public int score = 0;
    public Text scoreText = null;
	
	// Update is called once per frame
	void Update () {
        scoreText.text = string.Format("Score: {0}", score);
	}
}

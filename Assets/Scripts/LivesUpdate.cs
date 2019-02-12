using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LivesUpdate : MonoBehaviour {

	public int lives = 3;
    public Text livesText = null;
	
	// Update is called once per frame
	void Update () {
        livesText.text = string.Format("Lives: {0}", lives);
	}
}

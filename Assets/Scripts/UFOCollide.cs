using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollide : MonoBehaviour {

	List<int> scoreList = new List<int>{100, 150, 200, 250, 300};
	int myscore = 0;

	void OnCollisionEnter2D(Collision2D collision){
		 if(collision.collider.CompareTag("Projectile"))
        {
 			int randScore = Random.Range(0,5);
 			myscore = scoreList[randScore];
			GameObject.Find("Main Camera").GetComponent<ScoreUpdate>().score += myscore;
            Destroy(this.gameObject);
        }
         if(collision.collider.CompareTag("OuterWall"))
        {
 			Destroy(this.gameObject);
        }
	}
}
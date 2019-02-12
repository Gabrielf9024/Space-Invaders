using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

	public GameObject UFOPrefab;
	public float spawnnext = 60f;
	float timePassed = 0f;
	float travelSpeed = 5f;
	List<int> scoreList = new List<int>{100, 150, 200, 250, 300};
	
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed > spawnnext){
			timePassed = 0;
			if (SpawnNow()){SpawnUFO();}
		}
	}
	void SpawnUFO(){
		int randDirection = Random.Range(0,2);
		switch(randDirection){

			case 0:
				Vector2 position = new Vector2(-9f, 4.2f);
				GameObject eUFO = Instantiate(UFOPrefab,position, Quaternion.identity);
				Vector2 currentPosition = eUFO.GetComponent<Rigidbody2D>().velocity;
				eUFO.GetComponent<Rigidbody2D>().velocity = new Vector2(travelSpeed , currentPosition.y);
				break;

			case 1:
				Vector2 position2 = new Vector2(9f, 4.2f);
				GameObject neUFO = Instantiate(UFOPrefab,position2, Quaternion.identity);
				Vector2 currentPosition2 = neUFO.GetComponent<Rigidbody2D>().velocity;
				neUFO.GetComponent<Rigidbody2D>().velocity = new Vector2(-travelSpeed , currentPosition2.y);
				break;

			default:
				randDirection = 0;
				break;
		}
	}
	bool SpawnNow(){
		float randSpawn = Random.Range(0f,1f);
		if (randSpawn > .75){return true;}
		return false;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag("Projectile")){
			Destroy(this.gameObject);
			int randScore = Random.Range(0,5);
			// scoreList[randScore];

		}
	}
}

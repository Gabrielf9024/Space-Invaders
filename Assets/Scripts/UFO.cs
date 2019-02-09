using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

	public GameObject UFOPrefab;
	public float spawnnext = 60f;
	float timePassed = 0f;
	float travelSpeed = 5f;
	
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed > spawnnext){
			timePassed = 0;
			if (SpawnNow()){SpawnUFO();}
		}
	}
	void SpawnUFO(){
		Vector2 position = new Vector2(-9f, 4.2f);
		GameObject eUFO = Instantiate(UFOPrefab,position, Quaternion.identity);
		Vector2 currentPosition = eUFO.GetComponent<Rigidbody2D>().velocity;
		eUFO.GetComponent<Rigidbody2D>().velocity = new Vector2(travelSpeed , currentPosition.y);

	}
	bool SpawnNow(){
		float randSpawn = Random.Range(0f,1f);
		if (randSpawn > .7){return true;}
		return false;
	}

	// void OnCollisionEnter2D(Collision2D collision){
		// if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Surprise") || collision.collider.CompareTag("Spike") || collision.collider.CompareTag("pot")|| collision.collider.CompareTag("wall")){
		// 	Destroy(UFOPrefab);
		// }
	// }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public List<GameObject> enemyPrefab;

	private int height = 5;
	private int width = 11;

	private float waitTime = 1f;
	private float timeElapsed = 0f;

	private List<List<GameObject>> matrix = new List<List<GameObject>>();

	// Use this for initialization
	void Start () {
		createEnemies();
		
	}
	
	// Update is called once per frame
	void Update () {
		enemyMovment();
	}
	void createEnemies(){

		for(int x = 0; x < width; x++){
			matrix.Add(new List<GameObject>());
			for (int y = 0; y < height; y++){
				Vector2 position = new Vector2(x,y);
				GameObject enemyObject = Instantiate(enemyPrefab[0], this.transform);
				enemyObject.transform.localPosition = position;
				matrix[x].Add(enemyObject); 
			}
		}
	}
	void enemyMovment(){
		// checkEndCols();
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= waitTime){
			for(int x = 0; x < width; x++){
				for (int y = 0; y < height; y++){
				}
			}
			timeElapsed = 0;
		}
	}
	void checkEndCols(){

	}
	bool checkWall(GameObject item){
		bool hitWall = item.GetComponent<collide>().WallHit;
		return hitWall;
	}
}

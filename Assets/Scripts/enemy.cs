﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public List<GameObject> enemyPrefab;

	private int height = 5;
	private int width = 11;

	private float waitTime = 1f;
	private float timeElapsed = 0f;

	private bool moveRight = true;

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
		timeElapsed += Time.deltaTime;
		int gone = 0;
		if (timeElapsed >= waitTime){
			if (moveRight){
				for(int x = 0; x < width; x++){
					gone = 0;
					for (int y = 0; y < height; y++){
						if (x == width-1){
							if (matrix[x][y] != null){
								if (checkWall(matrix[x][y])){
									moveRight = false;
								}
							}
							if (matrix[x][y] == null){
								gone += 1;
							}
							if(gone == height){
								width -=1;
								Debug.Log("yikes");
							}
						}
						if (matrix[x][y] != null){
							matrix[x][y].transform.Translate(1f,0f,0f);
						}
						if (x == width-1 && moveRight == false){
							movedown();
						}
					}
				}
			}
			if (moveRight == false){
				for(int x = 0; x < width; x++){
					gone = 0;
					for (int y = 0; y < height; y++){
						if (x == 0){

							if (matrix[x][y] != null){
								if (checkWall(matrix[x][y])){
									moveRight = true;
								}
							}
							if (matrix[x][y] == null){
								gone += 1;
							}
							if(gone == height){
								shiftleft();
								return;
							}
						}
					if (matrix[x][y] != null && moveRight == false){
						matrix[x][y].transform.Translate(-1f,0f,0f);
					}
					if (x == 0 && moveRight == true){
						movedown();
					}
				}
			}
		}
		timeElapsed = 0;
	}
}
	void shiftleft(){
		for(int x = 1; x < width; x++){
			for (int y = 0; y < height; y++){
				matrix[x-1][y] = matrix[x][y];
			}
		}
		width -=1;
	}
	void movedown(){
		for(int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				if (matrix[x][y] != null){
					matrix[x][y].transform.Translate(0f,-.1f,0f);
				}
			}
		}
	}
	bool checkWall(GameObject item){
		bool hitWall = item.GetComponent<collide>().WallHit;
		item.GetComponent<collide>().WallHit = false;
		return hitWall;
	}
}

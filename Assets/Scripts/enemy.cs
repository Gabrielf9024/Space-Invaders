﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public List<GameObject> enemyPrefab;

	public float ConstSpeed = 1f;
	public float gameSpeed = .5f;
    public float spawnnext = 1f;
    float timePassed = 0f;

    private int height = 5;
	private int width = 11;

	private float waitTime = 1f;
	private float timeElapsed = 0f;

	private bool moveRight = true;

	private int MatrixSize = 0;
	private int currentSize = 0;

	public List<List<GameObject>> matrix = new List<List<GameObject>>(); // Made this public so I can access individual enemies outside this script

	// Use this for initialization
	void Start () {
		createEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		enemyMovment();
		//IncreaseGameSpeed();
        checkEndGame();
        float randShoot = Random.Range(0f, 1f);

        timePassed += Time.deltaTime;
        if (timePassed > spawnnext)
        {
            timePassed = 0;
            if (randShoot >= .7) // NOT FINAL Pressing v makes a random enemy shoot. We need to make the enemies shoot on their own at random intervals and then shooting is 100% done
            {
                chooseRandomEnemyToFire();
            }
        }

        if ( Input.GetKeyDown(KeyCode.V) ) // *NOT FINAL* Pressing v makes a random enemy shoot. We need to make the enemies shoot on their own at random intervals and then shooting is 100% done
        {
            chooseRandomEnemyToFire();
        }
	}
	void createEnemies(){
        int count = 0;
		for(int x = 0; x < width; x++){
			matrix.Add(new List<GameObject>());
			for (int y = 0; y < height; y++){
				Vector2 position = new Vector2(x,y);
				GameObject enemyObject = Instantiate(enemyPrefab[0], this.transform);
				enemyObject.transform.localPosition = position;
                enemyObject.name = "enemy" + count.ToString();
                ++count;
				matrix[x].Add(enemyObject);
			}
		}
		MatrixSize = height * width;
	}
	void enemyMovment(){
		timeElapsed += Time.deltaTime;
		int gone = 0;
		int cSize = 0;
			if (moveRight== true){
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
							}
							if (moveRight == false){
								cSize = currentSize;
								movedown();

							}
						}
						if (matrix[x][y] != null && moveRight == true){
							// matrix[x][y].transform.Translate(1f,0f,0f);
							cSize+=1;
							Vector2 current_Velocity = matrix[x][y].GetComponent<Rigidbody2D>().velocity;
							matrix[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(gameSpeed * ConstSpeed, current_Velocity.y);
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
								// return;
							}
							if (moveRight == true){
								cSize = currentSize;
								movedown();
								// return;
							}
						}
					if (matrix[x][y] != null && moveRight == false){
						// matrix[x][y].transform.Translate(-1f,0f,0f);
						cSize+=1;
						Vector2 current_Velocity = matrix[x][y].GetComponent<Rigidbody2D>().velocity;
						matrix[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(-(gameSpeed * ConstSpeed), current_Velocity.y);
					}
				}
			}
		}
		currentSize = cSize;
		IncreaseGameSpeed(cSize);
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
	void IncreaseGameSpeed(int x){

		if (x == 50){
			gameSpeed = 1f;
		}
		if (x  == 27){
			gameSpeed = 2f;
		}
		if (x == 11){
			gameSpeed = 3f;
		}
		if (x == 6){
			gameSpeed = 4f;
		}
		if (x <= 3){
			gameSpeed = 6f;
		}
	}
	void checkEndGame(){
		if (currentSize == 0){
			Debug.Log("you won!");
		}
	}

    void chooseRandomEnemyToFire()
    {
        int colNum = Random.Range(0, width);
        List<GameObject> column = sortColumn(colNum);
        if (column.Count == 0)
        {
            chooseRandomEnemyToFire();
            return;
        }
        else
        {
            GameObject chosenOne = column[0];
            chosenOne.GetComponent<collide>().fire();
        }
    }

    List<GameObject> sortColumn(int columnIndex) // Doesn't really sort the column since they're already sorted, but it puts the lowest postioned enemy in index 0
    {
        List<GameObject> col = new List<GameObject>();
        int start = columnIndex * height;
        for( int i = 0; i < height; ++i ) // Makes a list of all enemy objects in the given column (in descending order of index);
        {
            int enemyIndex = start + i;
            GameObject chosen = GameObject.Find("enemy" + enemyIndex.ToString());
            if (chosen != null)
                col.Add(chosen);
        }
        return col;
    }

}

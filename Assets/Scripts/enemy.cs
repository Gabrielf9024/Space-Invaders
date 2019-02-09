using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public List<GameObject> enemyPrefab;

	public float ConstSpeed = 1f;
	public float gameSpeed = .5f; 

	private int height = 5;
	private int width = 11;

	private float waitTime = 1f;
	private float timeElapsed = 0f;

	private bool moveRight = true;

	private int MatrixSize = 0;
	private float currentSize = 0f;

	private List<List<GameObject>> matrix = new List<List<GameObject>>();

	// Use this for initialization
	void Start () {
		createEnemies();
		
	}
	
	// Update is called once per frame
	void Update () {
		enemyMovment();
		IncreaseGameSpeed();
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
		MatrixSize = height * width;
	}
	void enemyMovment(){
		timeElapsed += Time.deltaTime;
		int gone = 0;
		currentSize =0;
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
								movedown();

							}
						}
						if (matrix[x][y] != null && moveRight == true){
							// matrix[x][y].transform.Translate(1f,0f,0f);
							currentSize+=1;
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
								return;
							}
							if (moveRight == true){
								movedown();
								// return;
							}
						}
					if (matrix[x][y] != null && moveRight == false){
						// matrix[x][y].transform.Translate(-1f,0f,0f);
						currentSize+=1;
						Vector2 current_Velocity = matrix[x][y].GetComponent<Rigidbody2D>().velocity;
						matrix[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(-(gameSpeed * ConstSpeed), current_Velocity.y);
					}
				}
			}
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
	void IncreaseGameSpeed(){

		if (currentSize == 50){
			gameSpeed = 1f;
		}
		if (currentSize  == 27){
			gameSpeed = 2f;
		}
		if (currentSize == 11){
			gameSpeed = 3f;
		}
		if (currentSize == 6){
			gameSpeed = 4f;
		}
	}

}

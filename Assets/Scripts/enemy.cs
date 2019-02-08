using System.Collections;
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
		// checkEndCols();
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= waitTime){
			if (moveRight == true){
				for(int x = 0; x < width; x++){
					for (int y = 0; y < height; y++){
						if (checkWall(matrix[width -1][height-1])){
							movedown();
							moveRight = false;
						}
						if (moveRight){
							matrix[x][y].transform.Translate(1,0,0);
						}
					}
				}
			}
			else{
				for(int x = 0; x < width; x++){
					for (int y = 0; y < height; y++){
						if (checkWall(matrix[0][height-1])){
							movedown();
							moveRight = true;
						}
						if (!moveRight){
							matrix[x][y].transform.Translate(-1,0,0);
						}
					}
				}
			}
			timeElapsed = 0;
		}
	}
	void movedown(){
		for(int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				matrix[x][y].transform.Translate(0f,-.1f,0f);
			}
		}
	}
	bool checkWall(GameObject item){
		bool hitWall = item.GetComponent<collide>().WallHit;
		item.GetComponent<collide>().WallHit = false;
		return hitWall;
	}
	void OnCollisionEnter2D(Collision2D collision){
		 if(collision.collider.CompareTag("Projectile") )
        {
            // Kill enemy here
            Destroy(this.gameObject);
        }
	}
}

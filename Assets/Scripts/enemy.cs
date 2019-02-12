using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour {

	public List<GameObject> enemyPrefab;

	public float ConstSpeed = 1f;
	public float gameSpeed = .5f;

    public float spawnnext = 4f;

    private float timePassed = 0f;
    private float totalGameTime = 0f;

    private int height = 5;
	private int width = 11;

	private float waitTime = 1f;
	private float timeElapsed = 0f;

	private float shootRate = .7f;

	private bool moveRight = true;

	public int MatrixSize = 0;

	public List<List<GameObject>> matrix = new List<List<GameObject>>(); // Made this public so I can access individual enemies outside this script

	// Use this for initialization
	void Start () {
		createEnemies();
		MatrixSize = 55;
	}
	
	// Update is called once per frame
	void Update () {
		enemyMovment();
        float randShoot = Random.Range(0f, 1f);

        timePassed += Time.deltaTime;
        // totalGameTime += Time.deltaTime;
        // if (timePassed > spawnnext)
        // {
        //     timePassed = 0;
        //     if (randShoot >= shootRate){
        //         chooseRandomEnemyToFire();
        //     }
        // }
        IncreaseDiff(totalGameTime);
	}
	void createEnemies(){
        int count = 0;
		for(int x = 0; x < width; x++){
			matrix.Add(new List<GameObject>());
			for (int y = 0; y < height; y++){
				Vector2 position = new Vector2(x,y);
				if (y == 4){
					GameObject enemyObject = Instantiate(enemyPrefab[0], this.transform);
					enemyObject.transform.localPosition = position;
	                enemyObject.name = "enemy" + count.ToString();
	              	enemyObject.GetComponent<collide>().myScore = 40;
	                ++count;
					matrix[x].Add(enemyObject);
				}
				if (y == 3|| y == 2){
					GameObject enemyObject2 = Instantiate(enemyPrefab[1], this.transform);
					enemyObject2.transform.localPosition = position;
	                enemyObject2.name = "enemy" + count.ToString();
	                enemyObject2.GetComponent<collide>().myScore = 20;
	                ++count;
					matrix[x].Add(enemyObject2);
				}
				if (y == 0 || y == 1){
					GameObject enemyObject3 = Instantiate(enemyPrefab[2], this.transform);
					enemyObject3.transform.localPosition = position;
	                enemyObject3.name = "enemy" + count.ToString();
	                enemyObject3.GetComponent<collide>().myScore = 10;
	                ++count;
					matrix[x].Add(enemyObject3);
				}
			}
		}
	}
	void enemyMovment(){
		int gone = 0;
		checkEndState();
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
						}
						if (moveRight == true){
							movedown();
						}
					}
					if (matrix[x][y] != null && moveRight == false){
						Vector2 current_Velocity = matrix[x][y].GetComponent<Rigidbody2D>().velocity;
						matrix[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(-(gameSpeed * ConstSpeed), current_Velocity.y);
					}
				}
			}
		}
		Debug.Log(MatrixSize);
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
		float currentSpeed = gameSpeed;
		for(int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				if (matrix[x][y] != null){
					gameSpeed = 0f;
					matrix[x][y].transform.Translate(0f,-.1f,0f);
				}
			}
		}
		gameSpeed = currentSpeed;
	}
	bool checkWall(GameObject item){
		bool hitWall = item.GetComponent<collide>().WallHit;
		item.GetComponent<collide>().WallHit = false;
		return hitWall;
	}
	void IncreaseDiff(float time){
		if (time >= 30){
			gameSpeed = 1.5f;
		}
		if (time >= 60){
			gameSpeed = 2f;
			shootRate =.60f;
		}
		if (time >= 70){
			shootRate = .55f;
		}
		if (time >= 90){
			gameSpeed = 3f;
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
        column = null;
    }

    List<GameObject> sortColumn(int columnIndex) // Doesn't really sort the column since they're already sorted, but it puts the lowest postioned enemy in index 0
    {
        List<GameObject> col = new List<GameObject>();
        int start = columnIndex * height;
        for( int i = 0; i < height; ++i ) // Makes a list of all enemy objects in the given column (in descending order of index);
        {
            int enemyIndex = start + i;
            GameObject chosen = GameObject.Find("enemy" + enemyIndex.ToString());
            if (isValid(columnIndex,i) && chosen != null){
                col.Add(chosen);
            }
        }
        return col;
    }
    bool isValid(int x, int y){
    	if(matrix[x][y] == null){
    		Debug.Log("not valid");
    		return false;
    	}
    	return true;
    }
    void checkEndState(){
    	if (MatrixSize == 0){
    		// SceneManager.LoadScene("GameOverScreen");
    	}
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public int lives = 3;
    public float moveSpeed;
    public Transform projectilePrefab;
    Transform lastShot;


	private Rigidbody2D PlayerRB;

	// Use this for initialization
	void Start () {
		PlayerRB = this.GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMoved();
		if (Input.GetKeyDown(KeyCode.Space)){
            if(lastShot == null)
            {
                PlayerShot();
            }
        }
	}
	void PlayerMoved(){
		float moveInput = Input.GetAxisRaw("Horizontal");
		Vector2 moveVelocity = PlayerRB.velocity;
		PlayerRB.velocity = new Vector2(moveInput * moveSpeed, moveVelocity.y);
	}

    // Spawns a projectile roughly above player position
	void PlayerShot(){
        Vector3 oneAbove = this.transform.position + Vector3.up/3;
        lastShot = Instantiate(projectilePrefab, oneAbove, Quaternion.identity);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Projectile"))
        {
            --lives;
            GetComponent<Transform>().position = new Vector3(0, -4.2f, 0); // Move player back to starting point
        }
        if (lives == 0)
        {
            SceneManager.LoadScene("GameStart"); // Put the main level scene here
        }
    }
}

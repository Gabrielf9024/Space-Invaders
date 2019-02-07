using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	
	public float moveSpeed;
    public Transform projectilePrefab;
    Transform lastShot;


	private Rigidbody2D PlayerRB;
	private int Lives = 3;

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
}

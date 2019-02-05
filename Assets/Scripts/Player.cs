using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public float moveSpeed = 5f;
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
			PlayerShot();
		}
	}
	void PlayerMoved(){
		float moveInput = Input.GetAxisRaw("Horizontal");
		Vector2 moveVelocity = PlayerRB.velocity;
		PlayerRB.velocity = new Vector2(moveInput * moveSpeed, moveVelocity.y);
	}
	void PlayerShot(){

	}
}

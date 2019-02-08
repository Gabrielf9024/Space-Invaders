using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour {
	public bool WallHit = false;
	private Rigidbody2D rb;

	void Start(){
		rb = this.GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag("Border")){
			WallHit = true;
			Debug.Log(collision.collider.name);
		}
		else {
			WallHit = false;
		}
	}
}

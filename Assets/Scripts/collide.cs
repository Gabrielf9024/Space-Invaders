using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour {
	public bool WallHit = false;

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag("Border")){
			WallHit = true;
		}
		 if(collision.collider.CompareTag("Projectile") )
        {
            // Kill enemy here
            Destroy(this.gameObject);
        }
	}
}

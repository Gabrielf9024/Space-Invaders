using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollide : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision){
		 if(collision.collider.CompareTag("Projectile") || collision.collider.CompareTag("OuterWall") )
        {
            // Kill enemy here
            Destroy(this.gameObject);
        }
	}
}

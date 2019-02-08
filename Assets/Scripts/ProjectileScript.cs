using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;

    private BoxCollider2D coll;
    

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Transform>().Translate(new Vector3(0, speed));
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") )
        {
            // Kill enemy here
            // Destroy(collision.gameObject);
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("Border") )
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

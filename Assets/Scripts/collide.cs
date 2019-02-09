// I think this should be named something differently, and should be used for anything that an individual enemy can do.
// Maybe this one should have been named enemyScript.cs and the enemy.cs changed to enemyGrid.cs, but I don't think
// we should risk changing things like that at this point so I'll leave it :]

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour
{
    public bool WallHit = false;
    public Transform projectilePrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Border"))
        {
            WallHit = true;
        }
        if (collision.collider.CompareTag("Projectile"))
        {
            // Kill enemy here
            Destroy(this.gameObject);
        }
    }

    public void fire()
    {
        Vector3 oneBelow = this.transform.position - Vector3.up / 3;
        Instantiate(projectilePrefab, oneBelow, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collide : MonoBehaviour
{
    public bool WallHit = false;
    public int myScore = 0;
    public GameObject projectilePrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Border") )
        {
            WallHit = true;
        }
        if (collision.collider.CompareTag("Projectile"))
        {
        	GameObject.Find("Main Camera").GetComponent<ScoreUpdate>().score += myScore;
        	--GameObject.Find("EnemySpawns").GetComponent<enemy>().MatrixSize;
            Destroy(this.gameObject);
            //Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Ground") ||
        	collision.collider.CompareTag("Player"))
        {
        	SceneManager.LoadScene("GameOverScreen");
        }
    }

    public void fire()
    {
        Vector3 oneBelow = this.transform.position - Vector3.up / 3;
        Instantiate(projectilePrefab, oneBelow, Quaternion.identity);
    }
}

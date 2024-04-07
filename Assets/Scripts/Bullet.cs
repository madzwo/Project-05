using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public PlayerMovement playerMovement;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("sphereRobot");
        playerMovement = player.GetComponent<PlayerMovement>();

    }

    void Update()
    {
        damage = playerMovement.damage;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Spawner")
        {
            Debug.Log("hit spawner - damage: " + damage);
            Spawner spawnerScript = collision.gameObject.GetComponent<Spawner>();
            spawnerScript.TakeDamage(damage);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

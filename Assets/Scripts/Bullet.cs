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

    public void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject.tag == "playerBullet")
        {
            if(collision.gameObject.tag == "Spawner")
            {
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
        else if (this.gameObject.tag == "enemyBullet")
        {
            if(collision.gameObject.tag != "Enemy")
            {
                if(collision.gameObject.tag == "sphereRobot")
                {
                    playerMovement.TakeDamage();
                }

                Destroy(gameObject);
            }
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {

    }
}

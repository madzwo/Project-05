using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed; 
    public float followDistance;
    public float health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("sphereRobot");
        health = 1;

    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= followDistance)
            {
                Vector3 direction = player.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);
                transform.rotation = rotation;

                direction.Normalize(); 
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else 
            {
                
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "playerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}


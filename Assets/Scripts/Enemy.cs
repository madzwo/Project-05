using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed; 
    public float followDistance;
    public float stopDistance;

    public float health;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public float fireRate;
    private float timeTillFire;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("sphereRobot");
        health = 1;

        timeTillFire = fireRate;

    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);
            transform.rotation = rotation;

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= followDistance)
            {
                if(distanceToPlayer >= stopDistance)
                {
                    direction.Normalize(); 
                    transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                }
            }
            else
            {

            }

            if (timeTillFire <= 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(-firePoint.up * bulletSpeed, ForceMode.Force);
                timeTillFire = fireRate;
            }
            timeTillFire -= Time.deltaTime;
        }
    }

    // public void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.tag == "playerBullet")
    //     {
    //         Destroy(collision.gameObject);
    //         Destroy(gameObject);
    //     }
    // }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}


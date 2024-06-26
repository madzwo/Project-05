using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public float spawnRate;
    private float timeTillSpawn;
    public GameObject player;
    public float spawnDistance;

    public float health;
    public GameObject sphere;

    public GameObject deadSpawner;



    void Start()
    {
        timeTillSpawn = spawnRate;
        player = GameObject.FindGameObjectWithTag("sphereRobot");
        

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= spawnDistance)
        {
            if (timeTillSpawn <= 0f)
            {
                SpawnEnemy();
                timeTillSpawn = spawnRate;
            }
            timeTillSpawn -= Time.deltaTime;
        }
        
    }

    public void SpawnEnemy()
    {
        float rand = Random.Range(0f,3f);
        Transform spawnPoint = null;
        if(rand > 2f)
        {
            spawnPoint = spawnPoint1;
        }
        else if(rand > 1f)
        {
            spawnPoint = spawnPoint2;
        }
        else
        {
            spawnPoint = spawnPoint3;
        }
        Instantiate(enemy, spawnPoint.position, spawnPoint1.rotation);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        sphere.transform.localScale -= new Vector3(.1f,.1f,.1f);
        if (health <= 0)
        {
            Instantiate(deadSpawner, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

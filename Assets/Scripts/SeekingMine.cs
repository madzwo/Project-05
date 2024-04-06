using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingMine : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed; 
    public float followDistance;

    public Transform spawnPoint;


    void Start()
    {
        if (GameObject.FindGameObjectWithTag("sphereRobot") != null)
        {
            player = GameObject.FindGameObjectWithTag("sphereRobot");
        }
        else if (GameObject.FindGameObjectWithTag("squareRobot") != null)
        {
            player = GameObject.FindGameObjectWithTag("squareRobot");
        }
        else if (GameObject.FindGameObjectWithTag("pyramidRobot") != null)
        {
            player = GameObject.FindGameObjectWithTag("pyramidRobot");
        }
        
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint.position);

            if (distanceToPlayer <= followDistance && distanceFromSpawn < followDistance)
            {
                Vector3 direction = player.transform.position - transform.position;
                direction.Normalize(); 
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
            else 
            {
                Vector3 direction = spawnPoint.position - transform.position;
                direction.Normalize(); 
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
        
}

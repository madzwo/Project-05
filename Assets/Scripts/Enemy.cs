using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed; 
    public float followDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("sphereRobot");

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
}
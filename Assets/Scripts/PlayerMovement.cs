using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    Rigidbody rb;
    Vector3 moveDirection;
    float horizontalInput;
    float verticalInput;

    public int vel;
    public int dur;
    public int frt;
    public int dmg;

    public float velocity;
    public float durability;
    public float firerate;
    public float damage;

    public float health;

    public int clearence;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        vel = 1;
        dur = 1;
        frt = 1;
        dmg = 1;

        clearence = 1;

    }

    void Update()
    {
        velocity = 100f + (vel * 50f);
        durability = 5f + (dur - 1f); // (maxHealth)
        firerate = 5f + (frt - 1f);
        damage = 3f + (dmg - 1f);

        health = durability;

        MyInput();
        Fire();
    }

    private void FixedUpdate()
    {
        SpeedControl();
        MovePlayer();
    } 

    private void MyInput() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * velocity, ForceMode.Force); 
    }

    public void Fire() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("fire");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.up * bulletSpeed, ForceMode.Force);
        }
         
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > velocity)
        {
            Vector3 limitedVel = flatVel.normalized * velocity;
            rb.velocity = new Vector3(limitedVel.x, 0f, limitedVel.z);
        }
    }

    private void Respawn()
    {
        float temp;
        temp = velocity;
        velocity = 0;
        Vector3 spawnPoint = new Vector3(0f,7f,-70f);
        this.transform.position = spawnPoint;
        velocity = temp;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("baseSphere"))
        {
            health = durability;
        }
        if(collision.gameObject.CompareTag("durChip"))
        {
            Debug.Log("durChip collected");
            Destroy(collision.gameObject);
            dur += 1;
        }
        if(collision.gameObject.CompareTag("velChip"))
        {
            Debug.Log("velChip collected");
            Destroy(collision.gameObject);
            vel += 1;
        }
        if(collision.gameObject.CompareTag("frtChip"))
        {
            Debug.Log("frtChip collected");
            Destroy(collision.gameObject);
            frt += 1;
        }
        if(collision.gameObject.CompareTag("dmgChip"))
        {
            Debug.Log("dmgChip collected");
            Destroy(collision.gameObject);
            dmg += 1;
        }

        if(collision.gameObject.CompareTag("ScannerLvl1") && clearence >= 1)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl2") && clearence >= 2)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl3") && clearence >= 3)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl4") && clearence >= 4)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl5") && clearence >= 5)
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Lazer"))
        {
            Respawn();
        }
        if(collision.gameObject.CompareTag("LazerScanner"))
        {
            Destroy(collision.gameObject);
        }


        if(collision.gameObject.CompareTag("seekingMine"))
        {
            Respawn();
        }

    }

}

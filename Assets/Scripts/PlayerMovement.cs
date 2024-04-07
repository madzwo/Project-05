using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


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

    public int clearance;

    public TMP_Text clearanceText;
    public TMP_Text velText;
    public TMP_Text dmgText;
    public TMP_Text durText;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;

    private float timeTillFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        vel = 1;
        dur = 1;
        frt = 1;
        dmg = 1;

        clearance = 1;
        health = 5f;
        timeTillFire = 2f;
    }

    void Update()
    {
        velocity = 25f + (vel * 25f);
        durability = 5f + (dur - 1f); // (maxHealth)
        firerate = 2f - ((frt-1) * .4f);
        damage = 3f + (dmg - 1f);


        MyInput();
        Fire();
        UI();
    }

    public void UI()
    {
        clearanceText.text = "Security Clearance Lvl " + clearance;
        velText.text = "Velocity (.vel)  " + vel;
        dmgText.text = "Damage (.dmg)  " + dmg;
        durText.text = "Durability (.dur)  " + dur;
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
        if (timeTillFire <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(firePoint.up * bulletSpeed, ForceMode.Force);
            }
            timeTillFire = firerate;
        }
        timeTillFire -= Time.deltaTime;
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

    public void TakeDamage()
    {
        health -= 1f;
        if (health <= 0)
        {
            Respawn();
        }
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
        if(collision.gameObject.CompareTag("SecurityChip"))
        {
            Debug.Log("SecurityChip collected");
            Destroy(collision.gameObject);
            clearance += 1;
        }

        if(collision.gameObject.CompareTag("ScannerLvl1") && clearance >= 1)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl2") && clearance >= 2)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl3") && clearance >= 3)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl4") && clearance >= 4)
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ScannerLvl5") && clearance >= 5)
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


        if(collision.gameObject.CompareTag("End"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }


    }

}

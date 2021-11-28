using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Иногда возникает залипание движения. Блокируется передвижение по одной оси

public class PlayerController : MonoBehaviour
{    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject gun;
    private GameManager gameManager;
    private float speed = 10f;
    private float xBound = 6f;
    private float zUpperBound = 0f;
    private float zLowerBound = -15f;
    //private int hitPoints = 10;

    //private bool isAlive = true;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameManager.isGameActive)
        {
            MovePlayer();
            ConstrainPlayerPosition();
            Shoot();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateHP(1);
        }
        if (other.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }        
    }
    public void IsDead()
    {
        Destroy(gameObject);
    }    
    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed, Space.World);
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectilePrefab, gun.transform.position, gun.transform.rotation);
        }
    }

    //Prevent player from leaving game area
    private void ConstrainPlayerPosition()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zLowerBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLowerBound);
        }
        else if (transform.position.z > zUpperBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zUpperBound);
        }
    }
}

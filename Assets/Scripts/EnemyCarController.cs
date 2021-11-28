using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Левый коридор X(-15;-10) Z(-16;4)
// Правый коридор X(10;15) Z(-16;4)

// 1. Верхняя левая четверть X(-15;-10) Z(-4;4)
// 2. Верхняя правая четверть X(10;15) Z(-4;4)
// 3. Нижняя правая четверть X(10;15) Z(-16;-8)
// 4. Нижняя левая четверть X(-15;-10) Z(-16;-8)

public class EnemyCarController : MonoBehaviour
{    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject gun;
    private GameManager gameManager;
    private bool path = false;
    private int hitPoints = 10;
    private float speed = 5f;
    private float startShootDelay = 6f;
    private float shootInterval = 2f;
    private bool isAlive = true;

    public GameObject waypointPrefab;
    public float upperBound;
    public float bottomBound;
    public float leftBound;
    public float rightBound;
    public string waypointTag;    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpawnRandomWaypoint();
        waypointTag = waypointPrefab.gameObject.name;
        InvokeRepeating("Shoot", startShootDelay, shootInterval);
    }
        
    // Update is called once per frame
    void Update()
    {
        IsAliveCheck();
        CheckGameStatus();

        if (path && isAlive && gameManager.isGameActive)
        {
            GameObject nextWaypoint = GameObject.FindWithTag(waypointTag);
            MoveToWaypoint(nextWaypoint, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(waypointTag))
        {            
            Destroy(other.gameObject);
            path = false;
            
            StartCoroutine(SpawnCountdown());
        }
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            hitPoints--;
        }
    }
    IEnumerator SpawnCountdown()
    {
        float countdown = Random.Range(0.1f, 1f);

        yield return new WaitForSeconds(countdown);
        SpawnRandomWaypoint();
    }
    IEnumerator Blast()
    {
        int bullets = Random.Range(2, 6);

        for (int i = 0; i < bullets; i++)
        {
            yield return new WaitForSeconds(0.15f);
            Instantiate(projectilePrefab, gun.transform.position, gun.transform.rotation);
        }        
    }
    private void IsAliveCheck()
    {
        if (hitPoints == 0)
        {
            Destroy(gameObject);
            gameManager.UpdateScores(1);
        }
    }
    private void Shoot()
    {
        StartCoroutine(Blast());
    }    
    void MoveToWaypoint(GameObject waypoint, float movementSpeed)
    {
        Vector3 movementDirection = (waypoint.transform.position - transform.position).normalized;

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
    private Vector3 GetRandomPosXZ(float xMin, float xMax, float zMin, float zMax)
    {
        float xRand = Random.Range(xMin, xMax);
        float zRand = Random.Range(zMin, zMax);

        return new Vector3(xRand, waypointPrefab.transform.position.y, zRand);
    }
    private void SpawnRandomWaypoint()
    {
        Instantiate(waypointPrefab, GetRandomPosXZ(leftBound, rightBound, bottomBound, upperBound), waypointPrefab.transform.rotation);
        path = true;
    }
    void CheckGameStatus()
    {
        if (!gameManager.isGameActive)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private float playerStartXPos = 0f;
    private float playerStartZPos = -15f;

    //Obstacles
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject enemyCarPrefab;
    [SerializeField] private GameObject[] waypointPrefabs;
    private GameManager gameManager;
    private float obsSpawnPosX = 4f;
    private float obsSpawnPosZ = 35f;
    private float obsSpawnInterval = 2f;

    //EnemyCars
    private GameObject inst_enemy;
    private float enemySpawnInterval = 7f;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnObstacle", obsSpawnInterval, obsSpawnInterval);
        InvokeRepeating("SpawnEnemyCar", enemySpawnInterval, enemySpawnInterval);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void SpawnObstacle()
    {
        if (gameManager.isGameActive)
        {
            float xPos = 0;

            switch (Random.Range(1, 3))
            {
                case 1:
                    xPos = obsSpawnPosX;
                    break;
                case 2:
                    xPos = -obsSpawnPosX;
                    break;
            }

            Vector3 spawnPos = new Vector3(xPos, obstaclePrefab.transform.position.y, obsSpawnPosZ);

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }        
    }
    private void SpawnEnemyCar()
    {
        if (gameManager.isGameActive)
        {
            int randomQuarter = Random.Range(1, 5);
            inst_enemy = Instantiate(enemyCarPrefab, GetEnemySpawnPos(randomQuarter), enemyCarPrefab.transform.rotation) as GameObject;
            SetQuarterBounds(randomQuarter);
            SetWaypointPrefab(randomQuarter);
        }        
    }    
    public void PlayerRespawn()
    {
        Vector3 startPos = new Vector3(playerStartXPos, playerPrefab.transform.position.y, playerStartZPos);

        Instantiate(playerPrefab, startPos, playerPrefab.transform.rotation);
    }

    // 1 Quarter - upper and left X(-50) Z(13)
    // 2 Quarter - upper and right X(50) Z(13)
    // 3 Quarter - lower and right X(30) Z(-30)
    // 4 Quarter - lower and left X(-30) Z(-30)
    private Vector3 GetEnemySpawnPos(int quarter)
    {
        Vector3 enemySpawnPos;

        switch (quarter)
        {
            case 1:
                enemySpawnPos = new Vector3(-50, enemyCarPrefab.transform.position.y, 13);
                return enemySpawnPos;
            case 2:
                enemySpawnPos = new Vector3(50, enemyCarPrefab.transform.position.y, 13);
                return enemySpawnPos;
            case 3:
                enemySpawnPos = new Vector3(30, enemyCarPrefab.transform.position.y, -30);
                return enemySpawnPos;
            case 4:
                enemySpawnPos = new Vector3(-30, enemyCarPrefab.transform.position.y, -30);
                return enemySpawnPos;
            default:
                return new Vector3(0, enemyCarPrefab.transform.position.y, -23);
        }
    }
    private void SetQuarterBounds(int quarter)
    {
        switch (quarter)
        {
            case 1:
                inst_enemy.GetComponent<EnemyCarController>().upperBound = 4;
                inst_enemy.GetComponent<EnemyCarController>().bottomBound = -4;
                inst_enemy.GetComponent<EnemyCarController>().leftBound = -15;
                inst_enemy.GetComponent<EnemyCarController>().rightBound = -10;

                break;
            case 2:
                inst_enemy.GetComponent<EnemyCarController>().upperBound = 4;
                inst_enemy.GetComponent<EnemyCarController>().bottomBound = -4;
                inst_enemy.GetComponent<EnemyCarController>().leftBound = 10;
                inst_enemy.GetComponent<EnemyCarController>().rightBound = 15;

                break;
            case 3:
                inst_enemy.GetComponent<EnemyCarController>().upperBound = -8;
                inst_enemy.GetComponent<EnemyCarController>().bottomBound = -16;
                inst_enemy.GetComponent<EnemyCarController>().leftBound = 10;
                inst_enemy.GetComponent<EnemyCarController>().rightBound = 15;

                break;
            case 4:
                inst_enemy.GetComponent<EnemyCarController>().upperBound = -8;
                inst_enemy.GetComponent<EnemyCarController>().bottomBound = -16;
                inst_enemy.GetComponent<EnemyCarController>().leftBound = -15;
                inst_enemy.GetComponent<EnemyCarController>().rightBound = -10;

                break;
        }
    }
    private void SetWaypointPrefab(int quarter)
    {
        switch (quarter)
        {
            case 1:
                inst_enemy.GetComponent<EnemyCarController>().waypointPrefab = waypointPrefabs[0].gameObject;

                break;
            case 2:
                inst_enemy.GetComponent<EnemyCarController>().waypointPrefab = waypointPrefabs[1].gameObject;

                break;
            case 3:
                inst_enemy.GetComponent<EnemyCarController>().waypointPrefab = waypointPrefabs[2].gameObject;

                break;
            case 4:
                inst_enemy.GetComponent<EnemyCarController>().waypointPrefab = waypointPrefabs[3].gameObject;

                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
    private float speed = 35f;
    private float bottomBound = -24f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveOn();
        CheckGameStatus();
    }
    void MoveOn()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < bottomBound)
        {
            Destroy(gameObject);
        }
    }
    void CheckGameStatus()
    {
        if (!gameManager.isGameActive)
        {
            Destroy(gameObject);
        }
    }
}

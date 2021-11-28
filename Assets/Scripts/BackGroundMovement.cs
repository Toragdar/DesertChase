using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    private float speed = 35f;
    private Vector3 startPos;
    public float repeatLength;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        repeatLength = GetComponent<BoxCollider>().size.z;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
    }
    void MoveBackground()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (transform.position.z < -120)
            {
                transform.position = startPos;
            }
        }
    }
}

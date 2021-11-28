using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float speed = 40f;
    private float screenBound = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x < -screenBound || transform.position.x > screenBound || transform.position.z < -screenBound || transform.position.z > screenBound)
        {
            Destroy(gameObject);
        }
    }
}

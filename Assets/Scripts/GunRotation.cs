using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private GameObject cursor;

    private void Start()
    {
        cursor = GameObject.Find("Cursor");
    }
    private void Update()
    {
        LookAtCursor();
    }
    private void LookAtCursor()
    {
        Vector3 position = new Vector3(cursor.transform.position.x, transform.position.y, cursor.transform.position.z);
        transform.LookAt(position);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellaSPIN : MonoBehaviour
{
    public float speed = 160;
    
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
        
    }
    
}

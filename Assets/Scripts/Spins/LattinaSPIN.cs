using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattinaSPIN : MonoBehaviour
{
    public float speed = 80;

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}

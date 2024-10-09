using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneteSPIN : MonoBehaviour
{

    public float speed = 80;

    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }
}

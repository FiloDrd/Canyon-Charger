using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerHit : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject oggetto = gameObject;
        if (other.tag == "Player")
        {
            Destroy(oggetto);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private float speed = 1.5f;

    private void Start()
    {
        speed = 2f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isMagnetActive")))
        {
            if (other.CompareTag("Player"))
            {
                transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed);
            }
        }
    }

}

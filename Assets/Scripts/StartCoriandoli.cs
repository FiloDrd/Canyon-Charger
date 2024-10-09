using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCoriandoli : MonoBehaviour
{
    public ParticleSystem[] Coriandoli;

    private void Start()
    {
        for (int i = 0; i < Coriandoli.Length; i++)
        {
            Coriandoli[i].Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            
                Coriandoli[Random.Range(0,Coriandoli.Length)].Play();
            
        }
    }
}

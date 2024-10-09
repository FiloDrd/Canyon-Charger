using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingParticles : MonoBehaviour
{
    public GameObject particelle;

    private GameObject temp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            StartCoroutine(PlayParticles(other.gameObject));
            
        }
    }

    IEnumerator PlayParticles( GameObject ostacolo)
    {
        temp = Instantiate(particelle, new Vector3(ostacolo.transform.position.x, ostacolo.transform.position.y + 1f, ostacolo.transform.position.z), Quaternion.Euler(0f, 0f, 180f));
        //print("cerato");
        yield return new WaitForSeconds(1f);
        Destroy(temp);
    }
}

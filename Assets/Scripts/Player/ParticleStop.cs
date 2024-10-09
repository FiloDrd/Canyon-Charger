using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStop : MonoBehaviour
{

    public GameObject particelle;
    void Update()
    {
        //print(FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isPaused")));
       if(FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isPaused")) || FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")) || FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isCompleted")) )

        {
            particelle.SetActive(false);

        }
        else
        {

            particelle.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSkinSelector : MonoBehaviour
{

    public GameObject Giocatore;
    public GameObject activeSkin;

    public GameObject[] Skins;

    private void Awake()
    {
        
        
        activeSkin = Instantiate(Skins[PlayerPrefs.GetInt("nowActive")], new Vector3(-22.46f, 4.47f, -1.72f), Quaternion.Euler(0f, 0f, 0f), Giocatore.transform);
        activeSkin.transform.localScale = new Vector3(1.800881f, 1.800881f, 1.800881f);
        
    }
}

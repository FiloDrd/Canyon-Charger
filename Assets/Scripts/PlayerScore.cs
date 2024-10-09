using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform giocatore;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = giocatore.position.z.ToString("0");
    }
}

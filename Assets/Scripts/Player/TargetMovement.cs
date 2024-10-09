using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float gravity = 23.47f;             //gravità che affligge il corpo

    public float speedAccel = 1f;            //accelerazione del moto del giocatore in percentuale alla velocità iniziale

    public float startSpeed = 100f;                    //velocità di movimento iniziale

    private int i = 0;                          //contatore per i frame dell'accelerazione

    public int accelTime = 300;                 //tempo di accelerazione

    public float jumpPower = 85f;               //potenza di salto

    public float sensibSlider = 0;

    public float memSlider = 0;

    public float finalSpeed;

    Touch touch;

    [SerializeField]
    Rigidbody rb;

    private void Start()
    {
        //transform.position = new Vector3(-22f, transform.position.y, transform.position.z);
    }

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, startSpeed);
        finalSpeed = startSpeed;
    }


    void Update()
    {
        if (!(FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")) || FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isCompleted"))))
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, finalSpeed);
        }
        else if (FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")) || FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isCompleted")))
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        forwardAccel();

    }

    void forwardAccel()
    {
        
        if (PlayerPrefs.GetInt("isPaused") == 0 && FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))
        {
            i += 1;                                                             //incremento il contatore una volta per frame

        }
        if (i % accelTime == 0 && PlayerPrefs.GetInt("isPaused") == 0 && FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))        //controllo se il contatore è uguale a 600
        {
            finalSpeed += speedAccel;
        }
    }

    

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSecondo : MonoBehaviour
{
    #region Variables
    public float gravity = 23.47f;             //gravità che affligge il corpo

    public float speedAccel = 1f;            //accelerazione del moto del giocatore in percentuale alla velocità iniziale

    public float startSpeed = 50f;                    //velocità di movimento iniziale

    private int i = 0;                          //contatore per i frame dell'accelerazione

    public int accelTime = 300;                 //tempo di accelerazione

    public float jumpPower = 85f;               //potenza di salto

    public float sensibSlider = 0;

    public float memSlider = 0;

    public static float finalSpeed;

    [SerializeField]
    Rigidbody rb;

    public GameObject Player;
    public GameObject Horse;

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, startSpeed);
        finalSpeed = startSpeed;
        conta_salti = 0;
    }

    private int conta_salti;
    void Update()
    {
        if (transform.position.y >= 11f)
        {
            conta_salti = 0;
        }

        if (!FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, finalSpeed);
        }
        else if (FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))
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
        if (i % accelTime == 0 && PlayerPrefs.GetInt("isPaused") == 0 && FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))                                                   //controllo se il contatore è uguale a 600
        {
            finalSpeed += speedAccel;
        }
    }

    public void JumpButtonPress()
    {
        if (isGrounded.groundCheck(transform.position.y) && Time.timeScale != 0 && !FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))
        {
            if(conta_salti == 0)
            {
                rb.AddForce(0f, jumpPower * JumpMultiplier(), 0f, ForceMode.Impulse);
                conta_salti = 1;
            }
            

        }
    }

    void FixedUpdate()
    {
        if (transform.position.y > 0.6f)
        {
            rb.AddForce(Physics.gravity * gravity, ForceMode.Acceleration);
        }
    }

    Vector2 touchStartPosition;
    


    /*public bool swipeUp()
    {
        
        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
        {
            touchStartPosition = touch.position;
        }
        if(touch.position.y - touchStartPosition.y >= swipeValue && touch.phase == TouchPhase.Moved)
        {
            print(touch.position.y - touchStartPosition.y);
            print("true");
            return true;
        }
        else
        {
            print("false");
            return false;
        }
    }*/

    public float JumpMultiplier()
    {
        if (FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isLattinaActive")))
        {
            return 1.5f;
        }
        else
        {
            return 1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMove : MonoBehaviour
{


    public Rigidbody rig;

    Touch touch;

    public float MAXheight;

    public float max;
    // Start is called before the first frame update
    void Start()
    {
        max = 0;
        MAXheight = 16.4021f;
        transform.position = new Vector3(-22f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //print(max);
        if(transform.position.y > max)
        {
            max = transform.position.y;
        }
            sideMove();
        //Debug.Log(PlayerPrefs.GetFloat("sensibility"));
    }
    //transform.position.x != sensibSlider

    void sideMove()
    {

        if(Input.touchCount > 0 && Time.timeScale != 0 && !FunzioniUtili.IntToBool(PlayerPrefs.GetInt("isGameOver")))
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * PlayerPrefs.GetFloat("sensibility"),
                    transform.position.y,
                    transform.position.z);
            }
        }
        if (transform.position.x < -37f)
        {
            transform.position = new Vector3(-37f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > -7f)
        {
            transform.position = new Vector3(-7f, transform.position.y, transform.position.z);
        }
        /*if(transform.position.y > MAXheight)
        {
            transform.position = new Vector3(transform.position.x, MAXheight, transform.position.z);
            rig.AddForce(Physics.gravity * 50f, ForceMode.Acceleration);
        }*/
    }
    

    public void changeSens(float newSens)
    {
        PlayerPrefs.SetFloat("sensibility", newSens);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGrounded : MonoBehaviour
{
    public double posizioneY;    //posizione attuale 
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {


        posizioneY = System.Math.Round(transform.position.y, 3);
        //print("Posizione y " + posizioneY + " Ground level " + "2.518" + "is grounded" + groundCheck(transform.position.y));
       // print(groundCheck(transform.position.y));
    }

    public static bool groundCheck(double newPosY)
    {
        double groundMin = 2.4;
        double groundMax = 2.7;
        double posizioneY;
        posizioneY = System.Math.Round(newPosY, 3);
        if (posizioneY <= groundMax && posizioneY >= groundMin)
        {
            return true;
        }else
        {
            return false;
        }
    }

}

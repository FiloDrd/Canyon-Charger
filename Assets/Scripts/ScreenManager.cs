using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private CanvasScaler canvasLevel;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "LevelMode")
        {
            CanvasScalerManager(canvasLevel);
        }
            
    }

    public static void CanvasScalerManager(CanvasScaler canvas)
    {
        int height = Screen.height;
        int width = Screen.width;
        //print("height: " + height + " width: " + width);

        if(width > height)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            canvas.matchWidthOrHeight = 0;
        }
        

    }

    


  
}

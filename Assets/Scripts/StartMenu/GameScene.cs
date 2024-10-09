using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    
    public void GoToInfiniteMode()
    {
        SceneManager.LoadScene("InfiniteMode");
    }
    public void GoToLevelMode()
    {
        SceneManager.LoadScene("LevelMode");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("LevelMode");
    }

}

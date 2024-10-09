using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Cavallo7");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {

    }
}

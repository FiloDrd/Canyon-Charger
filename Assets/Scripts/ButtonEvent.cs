using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject DayMode;
    public GameObject NightMode;
    public GameObject PauseButton;
    public GameObject[] ScreenObjects;
    public GameObject player;
    public GameObject InputPanel;

    public static float timeDelay = 0.07f;

    public void Start()
    {
        PlayerPrefs.SetInt("isPaused", 0);
        int Match = PlayerPrefs.GetInt("matchNumber") + 1;
        PlayerPrefs.SetInt("matchNumber", Match);
        
    }
    public void stopTime()
    {
        Time.timeScale = 0;
    }
    public void openPausePanel()
    {
        pausePanel.SetActive(true);
        PauseButton.SetActive(false);
        buttonEventStart.StartMenuCanvasManager(ScreenObjects, false);
        PlayerPrefs.SetInt("isPaused",1);
        Time.timeScale = 0;

    }
    public void resume()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("isPaused", 0);
        pausePanel.SetActive(false);
        PauseButton.SetActive(true);
        buttonEventStart.StartMenuCanvasManager(ScreenObjects, true);
    }
    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void quitSetting()
    {
        settingsPanel.SetActive(false);
    }

    public void activateNightMode()
    {
        DayMode.SetActive(false);
        NightMode.SetActive(true);
    }

    public void activateDayMode()
    {
        DayMode.SetActive(true);
        NightMode.SetActive(false);
    }

    public void quitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void OpenInputPanel()
    {
        InputPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void CloseInputPanel()
    {
        InputPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}


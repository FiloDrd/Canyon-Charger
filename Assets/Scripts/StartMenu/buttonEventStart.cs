using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class buttonEventStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingsPanel;
    public GameObject DayMode;
    public GameObject NightMode;
    public GameObject[] mainScreenObjects;
    public GameObject skinSelectionPanel;
    public GameObject shopPanel;
    public GameObject skinVendingPanel;
    public GameObject levelMode;
    public GameObject endlessMode;
    public GameObject creditsPanel;
    public Text soldiStart;
    public Text soldiPower;
    public Text soldiVending;
    public Text soldiNegozio;
    public Text level;
    public Text topScore;

    public static bool isSelectionActive;

    public float timeDelay = 0.07f;
    public void stopTime()
    {
        Time.timeScale = 0;
        
    }

    public static void StartMenuCanvasManager(GameObject[] oggetti, bool val)
    {
        for(int i = 0; i < oggetti.Length; i++)
        {
            oggetti[i].SetActive(val);
        }
    }

    public void openSettings()
    {
        
        StartMenuCanvasManager(mainScreenObjects, false);
        settingsPanel.SetActive(true);
    }

    public void quitSetting()
    {

        StartMenuCanvasManager(mainScreenObjects, true);
        settingsPanel.SetActive(false);
    }

    public void openSkinSelection()
    {

        StartMenuCanvasManager(mainScreenObjects, false);
        skinSelectionPanel.SetActive(true);
        isSelectionActive = true;
    }

    public void quitSkinSelection()
    {

        StartMenuCanvasManager(mainScreenObjects, true);
        skinSelectionPanel.SetActive(false);
        isSelectionActive = false;
    }

    public void openShop()
    {

        StartMenuCanvasManager(mainScreenObjects, false);
        shopPanel.SetActive(true);
    }

    public void quitShop()
    {

        StartMenuCanvasManager(mainScreenObjects, true);
        shopPanel.SetActive(false);
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

    public void openSkinVending()
    {
        skinVendingPanel.SetActive(true);
        skinSelectionPanel.SetActive(false);
    }

    public void closeSkinVending()
    {
        skinVendingPanel.SetActive(false);
        skinSelectionPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void UpdateMoney()
    {
        soldiStart.text = PlayerPrefs.GetInt("money").ToString();
        soldiPower.text = PlayerPrefs.GetInt("money").ToString();
        soldiVending.text = PlayerPrefs.GetInt("money").ToString();
        soldiNegozio.text = PlayerPrefs.GetInt("money").ToString();
    }

    int cont = 0;
    private void Update()
    {
        if(cont == 30)
        {
            UpdateMoney();
            cont = 0;
        }
        cont++;
    }


    public GameObject restartPanel;
    private void Start()
    {
        UpdateMoney();
        PlayerPrefs.SetInt("mode",1);
        level.text = PlayerPrefs.GetInt("level").ToString();
        //topScore.text = PlayerPrefs.GetInt("highscore").ToString();
        if (!PlayerPrefs.HasKey("RiavvioIniziale"))
        {
            restartPanel.SetActive(true);
        }
        else
        {
            
            restartPanel.SetActive(false);
        }

    }

    public void Restartbutton()
    {
        
        PlayerPrefs.SetInt("RiavvioIniziale", 1);
        PlayerPrefs.SetInt("money", 0);
        PlayerPrefs.SetString("unlockedHorses", "10000000000000000");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelModeButton()
    {
        levelMode.SetActive(false);
        endlessMode.SetActive(true);
        PlayerPrefs.SetInt("mode", 1);
    }

    public void EndlessModeButton()
    {
        levelMode.SetActive(true);
        endlessMode.SetActive(false);
        PlayerPrefs.SetInt("mode", 2);
    }

}

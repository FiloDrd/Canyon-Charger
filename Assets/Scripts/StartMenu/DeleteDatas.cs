using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDatas : MonoBehaviour
{

    public GameObject confirmPanel;
    public GameObject settingsPanel;

    private string value;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 0f);
        }
        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 0f);
        }
        if (!PlayerPrefs.HasKey("sensibility"))
        {
            PlayerPrefs.SetFloat("sensibility", 0.0371f);
        }
        if (!PlayerPrefs.HasKey("unlockedHorses"))
        {
            PlayerPrefs.SetString("unlockedHorses", "10000000000000000");
            //10000000000000000
            //11111111111111111
        }
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
        if (!PlayerPrefs.HasKey("mode"))
        {
            PlayerPrefs.SetInt("mode", 1);
        }
        if (!PlayerPrefs.HasKey("nowActive"))
        {
            PlayerPrefs.SetInt("nowActive", 0);
        }
        if (!PlayerPrefs.HasKey("adTimerStart"))
        {
            PlayerPrefs.SetInt("adTimerStart", 0);
        }
        if (!PlayerPrefs.HasKey("timesToGo"))
        {
            PlayerPrefs.SetInt("timesToGo", 5);
        }
        if (!PlayerPrefs.HasKey("dailySpin"))
        {
            PlayerPrefs.SetInt("dailySpin", 0);
        }
        if (!PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetInt("money", 0);
        }
        if (!PlayerPrefs.HasKey("pow1"))
        {
            PlayerPrefs.SetInt("pow1", 0);
        }
        if (!PlayerPrefs.HasKey("pow2"))
        {
            PlayerPrefs.SetInt("pow2", 0);
        }
        if (!PlayerPrefs.HasKey("pow3"))
        {
            PlayerPrefs.SetInt("pow3", 0);
        }
        if (!PlayerPrefs.HasKey("pow4"))
        {
            PlayerPrefs.SetInt("pow4", 0);
        }
        if (!PlayerPrefs.HasKey("inputType"))
        {
            PlayerPrefs.SetInt("inputType", 1);
        }
        if (!PlayerPrefs.HasKey("matchNumber"))
        {
            PlayerPrefs.SetInt("matchNumber", 0);
            
        }
        
        
    }

    public void appearConfirmPanel()
    {
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void disappearConfirmPanel()
    {
        settingsPanel.SetActive(true);
        confirmPanel.SetActive(false);
    }

    public void deleteDatas()
    {
        print("valore :  "  + value);
        if(value == "CONFIRM" || value == "confirm")
        {
            print("entrato");
            DeletePlayerPrefs();
            Application.Quit();
        }
    }

    public void DeletePlayerPrefs()
    {
        
        PlayerPrefs.SetFloat("music", 0f);
        PlayerPrefs.SetFloat("SFX", 0f);
        PlayerPrefs.SetFloat("sensibility", 0.0371f);
        PlayerPrefs.SetString("unlockedHorses", "10000000000000000");
        PlayerPrefs.SetInt("matchNumber", 0);
        // print("pref" + PlayerPrefs.GetString("unlockedHorses"));
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("mode", 1);
        PlayerPrefs.SetInt("nowActive", 0);
        PlayerPrefs.SetInt("adTimerStart", 0);
        PlayerPrefs.SetInt("timesToGo", 5);
        PlayerPrefs.SetInt("dailySpin", 0);
        PlayerPrefs.SetInt("money", 0);
        PlayerPrefs.SetInt("pow1", 0);
        PlayerPrefs.SetInt("pow2", 0);
        PlayerPrefs.SetInt("pow3", 0);
        PlayerPrefs.SetInt("pow4", 0);
        PlayerPrefs.SetInt("inputType", 1);
        
        Application.Quit(); 
    }

    public void SoldiGratis()
    {
        int money = PlayerPrefs.GetInt("money") + 1000;
        PlayerPrefs.SetInt("money", money);
    }

    public void ReadString(string S)
    {
        value = S;
    }

}

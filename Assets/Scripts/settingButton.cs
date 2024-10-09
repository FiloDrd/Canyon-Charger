using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingButton : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject[] mainScreen;
    public GameObject confirmPanel;
    public float timeDelay = 0.1f;

    public void activateSettings()
    {
        settingsPanel.SetActive(true);
        buttonEventStart.StartMenuCanvasManager(mainScreen, false);

    }

    public void deactivateSettings()
    {
        settingsPanel.SetActive(false);
        buttonEventStart.StartMenuCanvasManager(mainScreen, true);
    }

    public void activateConfirmDelete()
    {
        confirmPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void quitConfirmDelete()
    {
        confirmPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }


    public void deleteAllDatas()
    {
        PlayerPrefs.DeleteAll();
    }

}

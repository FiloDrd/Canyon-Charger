using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanelManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject inputPanel;
    public GameObject block1;
    public GameObject block2;
    public GameObject JumpButton;

    public MovePrima inputTipo1;
    public MoveSecondo inputTipo2;

    private void Start()
    {
        if (PlayerPrefs.GetInt("inputType") == 1)
        {
            PressType1();
            
        }
        if (PlayerPrefs.GetInt("inputType") == 2)
        {
            PressType2();
            
        }
    }

    public void PressType1()
    {
        PlayerPrefs.SetInt("inputType", 1);
        Type1Manager(true);
        Type2Manager(false);
    }
    public void PressType2()
    {
        PlayerPrefs.SetInt("inputType", 2);
        Type1Manager(false);
        Type2Manager(true);
    }

    private void Type1Manager(bool value)
    {
        block1.SetActive(value);
        inputTipo1.enabled = value;
    }
    private void Type2Manager(bool value)
    {
        block2.SetActive(value);
        inputTipo2.enabled = value;
        JumpButton.SetActive(value);
    }

    public void OpenInputTypes()
    {
        inputPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void CloseInputTypes()
    {
        inputPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}

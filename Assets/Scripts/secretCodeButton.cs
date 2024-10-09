using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secretCodeButton : MonoBehaviour
{
    // Start is called before the first frame update
    private string value;
    public GameObject nightmode;
    public Text secretCode;
    public Text deleteCode;
    public int activated = 0;
    private void Start()
    {
        nightmode.SetActive( IntToBool( PlayerPrefs.GetInt("nightmode") ) );
    }

    public void buttonCheck()
    {
        
        if(value == "nightmode" || value == "Nightmode" || value == "nightMode" || value == "NightMode")
        {
            activated = 1;
            PlayerPrefs.SetInt("nightmode", activated);
            nightmode.SetActive(true);
        }
        secretCode.text = "";
    }

    public void deleteCheck()
    {

        if (value == "CONFIRM" || value == "confirm")
        {
            print("deleted");
            PlayerPrefs.DeleteAll();
            Application.Quit();

        }
        clearText(deleteCode);
    }
    public bool IntToBool(int n)
    {
        if( n == 1)
        {
            return true;
        }
        return false;
    }

    public void ReadString(string S)
    {
        value = S;
    }

    public void clearText(Text textField)
    {
        textField.text = "";
    }
}

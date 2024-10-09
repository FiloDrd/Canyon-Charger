using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerUpMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject powerUpPanel;
    public GameObject infoPanel;
    public GameObject[] mainScreenObjects;
    public Text pow1;
    public Text pow2;
    public Text pow3;
    public Text pow4;
    public Text cost1;
    public Text cost2;
    public Text cost3;
    public Text cost4;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public int costoBase = 10;
    public int maxLevel;

    private void Start()
    {
        costoBase = 10;
        maxLevel = 10;
    }

    public void getMoney()
    {
        int k = PlayerPrefs.GetInt("money") + 10000;
        PlayerPrefs.SetInt("money", k);
    }

    private void Update()
    {
        pow1.text = MaxLevel(PlayerPrefs.GetInt("pow1").ToString(), "livello");
        pow2.text = MaxLevel(PlayerPrefs.GetInt("pow2").ToString(), "livello");
        pow3.text = MaxLevel(PlayerPrefs.GetInt("pow3").ToString(), "livello");
        pow4.text = MaxLevel(PlayerPrefs.GetInt("pow4").ToString(), "livello");
        cost1.text = MaxLevel(getCost(PlayerPrefs.GetInt("pow1")).ToString(), "costo");
        cost2.text = MaxLevel(getCost(PlayerPrefs.GetInt("pow2")).ToString(), "costo");
        cost3.text = MaxLevel(getCost(PlayerPrefs.GetInt("pow3")).ToString(), "costo");
        cost4.text = MaxLevel(getCost(PlayerPrefs.GetInt("pow4")).ToString(), "costo");
        changeColor(cost1, "pow1");
        changeColor(cost2, "pow2");
        changeColor(cost3, "pow3");
        changeColor(cost4, "pow4");
        //print("livello: " + PlayerPrefs.GetInt("pow1").ToString() + "   costo: " + getCost(PlayerPrefs.GetInt("pow1")).ToString());
        maxBlock(cost1.text, Button1);
        maxBlock(cost2.text, Button2);
        maxBlock(cost3.text, Button3);
        maxBlock(cost4.text, Button4);
        //print(cost1.text);
    }

    public void maxBlock( string max , Button b)
    {
        if(max == "MAX")
        {
            b.interactable = false;
        }
        else
        {
            b.interactable = true;
        }
    }

    public string MaxLevel(string str, string tipo)
    { 
        if(tipo == "livello")
        {
            if(str == maxLevel.ToString())
            {
                return "MAX";
            }
        }
        else if( tipo == "costo")
        {
            if (str == getCost(maxLevel).ToString())
            {
                return "MAX";
            }
        }
        return str;
    }

    public void appearInfoPanel()
    {
        powerUpPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void disappearInfoPanel()
    {
        powerUpPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void appearMenu()
    {
        powerUpPanel.SetActive(true);
        buttonEventStart.StartMenuCanvasManager(mainScreenObjects, false);
        //mainScreenObjects.SetActive(false);
    }
    public void disappearMenu()
    {
        powerUpPanel.SetActive(false);
        buttonEventStart.StartMenuCanvasManager(mainScreenObjects, true);
        //mainScreenObjects.SetActive(true);
    }
    public void pressBuyPowerup1()
    {
        buyPower("pow1");
    }
    public void pressBuyPowerup2()
    {
        buyPower("pow2");
    }
    public void pressBuyPowerup3()
    {
        buyPower("pow3");
    }
    public void pressBuyPowerup4()
    {
        buyPower("pow4");
    }

    public void buyPower(string pwrUp)
    {
        if (getCost(PlayerPrefs.GetInt(pwrUp)) <= PlayerPrefs.GetInt("money"))
        {
            int money = PlayerPrefs.GetInt("money");
            PlayerPrefs.SetInt("money", money - getCost(PlayerPrefs.GetInt(pwrUp)));
            int level = PlayerPrefs.GetInt(pwrUp);
            PlayerPrefs.SetInt(pwrUp, level+1);
        }
    }

    public int getCost(int val)
    {
        int cost = costoBase;
        int i;

        for (i=0; i < val ;i++)
        {
            cost += cost;
        }

        return cost;
    }

    private void changeColor(Text text, string name)
    {
        if(getCost(PlayerPrefs.GetInt(name)) > PlayerPrefs.GetInt("money"))
        {
             text.color =new Color(0.794f, 0.01960784f, 0.01960784f);
        }
        else
        {
            text.color = Color.white;
        }
    }
}

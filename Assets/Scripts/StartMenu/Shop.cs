using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Variabili
    public List<int> costi;
    public int nComuniRare;
    public int nEpico;
    public int nLeggendario;

    [Tooltip("NON TOCCARE"), HideInInspector]
    public int index1;
    [Tooltip("NON TOCCARE"), HideInInspector]
    public int index2;

    public float Rotation_Speed;

    public Jsonreader jsonReader;

    public Text testo1;
    public Text testo2;

    public Text timerText;

    public Text costoPannello1;
    public Text costoPannello2;

    private GameObject cavallo1;
    private GameObject cavallo2;

    private GameObject cavallo1Pannello;
    private GameObject cavallo2Pannello;

    public GameObject casella1;
    public GameObject casella2;

    public GameObject spiga1;
    public GameObject spiga2;

    public Button button1;
    public Button button2;

    public CanvasScaler canvas;

    #endregion

    #region Variabili Pannelli

    public GameObject Pannello1;
    public GameObject Pannello2;

    #endregion

    #region Generate indexes
    public int generaPrimoCavallo()
    {
        /*int gen = 0;
        double deltaSkin = nEpicoLeggendario - nComuniRare;
        double giorno = FunzioniUtili.GetDateValue("day");
        double L = 1 -(
            System.Math.Pow(giorno - 32, 2)
            /
            (32*32));

        double temp;

        temp = 0 - nEpicoLeggendario + System.Math.Sqrt( deltaSkin * deltaSkin * L);
        gen = System.Math.Abs((int)System.Math.Round(temp, 0)) - 1;
        return gen-1;*/

        Random.InitState(FunzioniUtili.GetDateValue("day"));

        int gen = Random.Range(1, nComuniRare + nEpico- 1);

        if(FunzioniUtili.GetDateValue("day") == 1)
        {
            gen = Random.Range(nComuniRare + nEpico, nLeggendario-1);
        }

        return gen;

    }

    public int generaSecondoCavallo()
    {
        
        /*double deltaSkin = nEpicoLeggendario - nComuniRare;
        double giorno = FunzioniUtili.GetDateValue("day");
        double L = 1 - (
            System.Math.Pow(giorno - 32, 2)
            /
            (32 * 32));

        double temp;

        temp = 0 - nEpicoLeggendario - System.Math.Sqrt(deltaSkin * deltaSkin * L);
        gen = System.Math.Abs((int)System.Math.Round(temp, 0)) - 1;*/

        Random.InitState(FunzioniUtili.GetDateValue("day") * 7);

        int gen = Random.Range(1, nComuniRare + nEpico - 1);
            while(gen == index1)
            {
                gen = Random.Range(1, nComuniRare + nEpico - 1);
            }
        return gen;

    }
    #endregion

    public string GetCost(string rarity)
    {
        if (rarity == "Rarity: ★☆☆☆")
        {
            return costi[0].ToString();
        }
        if (rarity == "Rarity: ★★☆☆")
        {
            return costi[1].ToString();
        }
        if (rarity == "Rarity: ★★★☆")
        {
            return costi[2].ToString();
        }
        if (rarity == "Rarity: ★★★★")
        {
            return costi[3].ToString();
        }
        return "";
    }

    public string TextUpdater(string str, int index, GameObject spiga, Button pulsante)
    {
        string cavalliSbloccati = PlayerPrefs.GetString("unlockedHorses");
        if (FunzioniUtili.ReadString(cavalliSbloccati, index) == '0')
        {
            pulsante.interactable = true;
            //spiga.SetActive(true);
            return str;
            
        }
        else
        {
            pulsante.interactable = false;
            spiga.SetActive(false);
            return "owned";
        }
        
    }

    public void HorseUnlocker(int index)
    {
        string str = PlayerPrefs.GetString("unlockedHorses");
        PlayerPrefs.SetString("unlockedHorses", FunzioniUtili.ModifyString(str, '1', index));
    }

    public void RemoveMoney(int index)
    {
        int costo = int.Parse(GetCost(jsonReader.mySkinList.skinData[index].rarity));
        int soldi = PlayerPrefs.GetInt("money") - costo;
        PlayerPrefs.SetInt("money", soldi);
        
    }

    private void ChangeColor(Text text, int indice, string type)
    {
        
        if (int.Parse(GetCost(jsonReader.mySkinList.skinData[indice].rarity)) > PlayerPrefs.GetInt("money"))
        {
            text.color = new Color(0.794f, 0.01960784f, 0.01960784f);
        }
        else
        {
            text.color = Color.white;
            if (type == "casella")
            {
                text.color = Color.white;
            }  
            else if (type == "pannello")
            {
                text.color = new Color(0.5137255f, 0.2627451f, 0.1490196f);
            }
                
        }
    }

    public void UpdateReverseTimer(Text timerTextF)
    {
        int ore = 23 - FunzioniUtili.GetDateValue("hour");
        int minuti = 59 - FunzioniUtili.GetDateValue("minutes");
        int secondi = FunzioniUtili.GetDateValue("seconds");
        //print(FunzioniUtili.GetDateValue("hour") + "     " + FunzioniUtili.GetDateValue("minutes"));
        switch (secondi%2)
        {
            case 0:
                if(minuti / 10 == 0)
                {
                    timerTextF.text = "NEW OFFERS AVAILABLE IN: " + ore.ToString() + ":0" + minuti.ToString();
                }
                else
                {
                    timerTextF.text = "NEW OFFERS AVAILABLE IN: " + ore.ToString() + ":" + minuti.ToString();
                }
                
                break;
            case 1:
                if (minuti / 10 == 0)
                {
                    timerTextF.text = "NEW OFFERS AVAILABLE IN: " + ore.ToString() + " 0" + minuti.ToString();
                }
                else
                {
                    timerTextF.text = "NEW OFFERS AVAILABLE IN: " + ore.ToString() + " " + minuti.ToString();
                }
                break;
        }
        
    }

    #region Button Events
    public void Click_Casella(GameObject pannello)
    {
        spiga1.SetActive(false);
        spiga2.SetActive(false);
        pannello.SetActive(true);
        cavallo1.SetActive(false);
        cavallo2.SetActive(false);
    }

    public void ClosePanello(GameObject pannello)
    {

        if (testo1.text != "owned")
        {
            spiga1.SetActive(true);
        }
        if (testo2.text != "owned")
        {
            spiga2.SetActive(true);
        }


        pannello.SetActive(false);
        cavallo1.SetActive(true);
        cavallo2.SetActive(true);
    }

    public void BuyHorse1(GameObject pannello)
    {
        if(int.Parse(GetCost(jsonReader.mySkinList.skinData[index1].rarity)) <= PlayerPrefs.GetInt("money"))
        {
            HorseUnlocker(index1);
            RemoveMoney(index1);
            ClosePanello(pannello);
        }
    }
    public void BuyHorse2(GameObject pannello)
    {
        if (int.Parse(GetCost(jsonReader.mySkinList.skinData[index2].rarity)) <= PlayerPrefs.GetInt("money"))
        {
            HorseUnlocker(index2);
            RemoveMoney(index2);
            ClosePanello(pannello);
        }
    }

    #endregion

    private void Start()
    {
        index1 = generaPrimoCavallo();
        index2 = generaSecondoCavallo();

        costi = new List<int> { 200, 400, 1000, 3000 };



        

        jsonReader = GameObject.Find("SkinManager").GetComponent<Jsonreader>();


        cavallo1 = Instantiate(jsonReader.Horses[index1], new Vector3(-24.72037f, 46.85751f, -90.32437f), Quaternion.Euler(new Vector3(0f, 90f, 31.361f)), casella1.transform);
        cavallo2 = Instantiate(jsonReader.Horses[index2], new Vector3(-24.47788f, 46.85751f, -90.32437f), Quaternion.Euler(new Vector3(0f, 90f, 31.361f)), casella2.transform);

        cavallo1Pannello = Instantiate(jsonReader.Horses[index1], new Vector3(-24.58488f, 46.74812f, -90.5633f), Quaternion.Euler(new Vector3(0f, -90f, -50.35f)), Pannello1.transform);
        if (index1 >= 7 && index1 <= 15)
        {
            cavallo1Pannello.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f); // new Vector3(98.94878f, 98.94878f, 98.94878f);
        }
        else
        {
            cavallo1Pannello.transform.localScale = new Vector3(98.94878f, 98.94878f, 98.94878f);
        }



        cavallo2Pannello = Instantiate(jsonReader.Horses[index2], new Vector3(-24.58488f, 46.74812f, -90.5633f), Quaternion.Euler(new Vector3(0f, -90f, -50.35f)), Pannello2.transform);
        if (index2 >= 7 && index2 <= 15)
        {
            cavallo2Pannello.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f); // new Vector3(98.94878f, 98.94878f, 98.94878f);
        }
        else
        {
            cavallo2Pannello.transform.localScale = new Vector3(98.94878f, 98.94878f, 98.94878f);
        }

        ScreenManager.CanvasScalerManager(canvas);


        Rotation_Speed = 30f;

        

        

    }

    private void Update()
    {
        
        SkinVending.Spin(cavallo1, 30f);
        SkinVending.Spin(cavallo2, 30f);
        SkinVending.Spin(cavallo1Pannello, 30f);
        SkinVending.Spin(cavallo2Pannello, 30f);


        testo1.text = TextUpdater(GetCost(jsonReader.mySkinList.skinData[index1].rarity), index1, spiga1, button1);
        testo2.text = TextUpdater(GetCost(jsonReader.mySkinList.skinData[index2].rarity), index2, spiga2, button2);
        costoPannello1.text = testo1.text;
        costoPannello2.text = testo2.text;

        ChangeColor(testo1, index1, "casella");
        ChangeColor(testo2, index2, "casella");
        ChangeColor(costoPannello1, index1, "pannello");
        ChangeColor(costoPannello2, index2, "pannello");

        UpdateReverseTimer(timerText);

        //print(index1);
        //print(index2);
    }
}

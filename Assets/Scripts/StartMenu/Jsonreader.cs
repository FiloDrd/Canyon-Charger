using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jsonreader : MonoBehaviour
{

    public TextAsset textJSON;      //file json con i dati del cavallo

    public GameObject[] Horses;     //array contenente i cavalli
    public GameObject[] HorsesDaSpawnare;     //array contenente i cavalli da spawnare
    public GameObject[] Locks;     //array contenente i lucchetti
    public GameObject walkingHorse;
    public GameObject[] mainScreenObjects;
    public GameObject skinSelectionPanel;
    public GameObject cavalloAttivo;

    private float screenCenter = -24.6f;
    private float selectedZone = 0.1625f;
    public char[] unlockedHorses;

    public Text horseName;
    public Text horseRarity;

    private int selected = 0;


    private Vector3 horseSpawnPosition = new Vector3(-31.4f, 3.88f, -67.3f);
    private Vector3 horseSpawnPositionBis = new Vector3(-31.4f, 8.74f, -67.3f);

    #region Classes

    [System.Serializable]
    public class Skin_data      //classe con gli attributi di ogni skin
    {
        public int ID;
        public string name;
        public string rarity;
        public bool isUnlocked;
    }

    [System.Serializable]
    public class skinList           //classe di array di elementi della classe Skin_data
    {
        public Skin_data[] skinData;
    }
    
    public skinList mySkinList = new skinList();        
    #endregion
    void Start()
    {

        horseSpawnPosition = new Vector3(-31.4f, 3.88f, -67.3f);
        horseSpawnPositionBis = new Vector3(-31.4f, 8.74f, -67.3f);
    mySkinList = JsonUtility.FromJson < skinList > (textJSON.text); //ricevo i dati dal file json
        summonHorse();
        UpdateUnlockedHorses();
        //print(mySkinList.skinData.Length);
    }

    // Update is called once per frame
    void Update()
    {
        #region Selection Panel Manager
        //print("posizione cavallo1: " + System.Math.Round(Horses[0].transform.position.x, 4) + "    posizione cavallo2: " + System.Math.Round(Horses[1].transform.position.x, 4));
        //print("lunghezza skin list: "+ mySkinList.skinData.Length);
        if (buttonEventStart.isSelectionActive)
        {
            for (int i = 0; i < mySkinList.skinData.Length; i++)                     // mySkinList.skinData.Length prende la lunghezza dal file json *ATTENZIONE ALL'INSERIMENTO DI NUOVE SKINS*
            {
                //print(i + "    " + checkSelection(Horses[i]));
                if (checkSelection(Horses[i], i))
                {

                    //print("selezionato il " + i);

                    horseName.text = mySkinList.skinData[i].name;

                    horseRarity.text = mySkinList.skinData[i].rarity;

                    if (mySkinList.skinData[i].isUnlocked)
                    {
                        horseRotator(Horses[i]);
                    }
                       
                }
                if (!checkSelection(Horses[selected], selected))
                {
                    Horses[selected].transform.rotation = Quaternion.Euler(0f, 90f, 50.04935f);
                    selected = i;
                }
                
                if (mySkinList.skinData[i].isUnlocked)
                {
                    Locks[i].SetActive(false);
                }
                else
                {
                    Locks[i].SetActive(true);
                }
            }
            //print(Horses[0].transform.position.x - Horses[1].transform.position.x);
        }
        #endregion
        
        //if(Horses[selected].transform.position.x <=)
        //print(PlayerPrefs.GetString("unlockedHorses"));

        //print("0: " + mySkinList.skinData[0].isUnlocked + "1: " + mySkinList.skinData[1].isUnlocked + "2: " + mySkinList.skinData[2].isUnlocked + "3: " + mySkinList.skinData[3].isUnlocked +
        //    "4: " + mySkinList.skinData[4].isUnlocked + "5: " + mySkinList.skinData[5].isUnlocked + "6: " + mySkinList.skinData[6].isUnlocked + "7: " + mySkinList.skinData[7].isUnlocked);
    }

    public void UpdateUnlockedHorses()
    {
        unlockedHorses = PlayerPrefs.GetString("unlockedHorses").ToCharArray();
        for (int i = 0; i < mySkinList.skinData.Length; i++)
        {

            mySkinList.skinData[i].isUnlocked = charToBool(unlockedHorses[i]);

        }
    }

    public static bool charToBool( char c )
    {
        if ( c == '1')
        {
            return true;
        }
        else if( c == '0')
        {
            return false;
        }
        return false;
    }

    #region Skin Panel Functions
    public bool checkSelection(GameObject elem, int index) //controlla se il cavallo è selezionato
    {

        if (elem.transform.position.x < (screenCenter + selectedZone) && elem.transform.position.x > (screenCenter - selectedZone))
        {
            return true;
        }
        return false;

        /*if (index <= 6 || index == 16)
        {
            if (elem.transform.position.x < (screenCenter + selectedZone) && elem.transform.position.x > (screenCenter - selectedZone))
            {
                return true;
            }
            return false;
        }
        else
        {
            if (elem.transform.position.x < (screenCenter) && elem.transform.position.x > (screenCenter - 2*selectedZone))
            {
                return true;
            }
            return false;
        }*/
            
    }

    public void selectHorse() // seleziona un determinato cavallo
    {
        //print("ID skin: " + mySkinList.skinData[selected].ID + " skin selezionata: " + selected + " isUnocked: " + mySkinList.skinData[selected].isUnlocked);
        if (mySkinList.skinData[selected].isUnlocked)
        {
            PlayerPrefs.SetInt("nowActive", mySkinList.skinData[selected].ID);

            
            buttonEventStart.StartMenuCanvasManager(mainScreenObjects, true);
            skinSelectionPanel.SetActive(false);
            buttonEventStart.isSelectionActive = false;
            destroyHorse();
            summonHorse();
        }
        
    }

    public void horseRotator(GameObject cavallo) //fa girare il cavallo costantemente
    {
        cavallo.transform.Rotate(Vector3.up * Time.fixedDeltaTime * 15f);

    }

    #endregion

    #region Start Menu Effects
    public void summonHorse() //crea il cavallo nella schemata home
    {
       if(PlayerPrefs.GetInt("nowActive")>=7 && PlayerPrefs.GetInt("nowActive") <= 14)
        {
            cavalloAttivo = Instantiate(HorsesDaSpawnare[PlayerPrefs.GetInt("nowActive")], horseSpawnPositionBis, Quaternion.Euler(Vector3.zero));
        }
        else
        {
            cavalloAttivo = Instantiate(HorsesDaSpawnare[PlayerPrefs.GetInt("nowActive")], horseSpawnPosition, Quaternion.Euler(Vector3.zero));
        }
        
        //print(PlayerPrefs.GetInt("nowActive"));
        cavalloAttivo.transform.localScale = new Vector3(1.800881f, 1.800881f, 1.800881f);
        Animator chillingCavallo;
        chillingCavallo = cavalloAttivo.GetComponent<Animator>();
        chillingCavallo.enabled = true;
    }

    public void destroyHorse() //elimina il cavallo nella schemata home
    {
        Destroy(cavalloAttivo);
    }
    #endregion

    //TODO nell'update fai in modo che modifichi in tempo reale le skin sbloccate e quelle non sbloccate tramite la stringa "unlockedHorses"
}

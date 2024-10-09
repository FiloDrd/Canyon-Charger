using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinVending : MonoBehaviour
{

    public Animator anim_macchinetta;
    public Text testo_prezzo;
    public GameObject Cavallo_da_spawnare;
    public GameObject Pulsante;
    public Text nomeCavallo;
    public Text Rarita;
    public GameObject testoConsolazione;
    public GameObject nuovoCavalloTesto;

    public Jsonreader jsonReader;

    public int volte_premuto;
    public int prezzo = 100;
    public int soldiConsolazione;
    public float StartTime;
    private float duration;
    private bool isPlaying;
    public bool exist;
    public string newString;
    public int[] soldiStandardConsolazione;
 
    private void Start()
    {
        Random.InitState((int)(Time.time * 1000000));

        volte_premuto = 0;
        StartTime = -5.5f;
        anim_macchinetta = GetComponent<Animator>();
        duration = 3.8f; //durata dell'animazione
        isPlaying = true;
        dentroIF = 0;
        fuoriIF = 0;
        nomeCavallo.text = " ";
        Rarita.text = " ";
        
    }



    public void buyButtonPress(string str) //chiamata quando viene premuto il pulsante per comprare la skin
    {
        
        if(Time.time - StartTime >= duration)
        {

            if(PlayerPrefs.GetInt("money") >= prezzo && str == "money")
            {
                 
                compraSkin(0);
                volte_premuto++;
                nVolte = 0;
                fuoriIF = dentroIF;
                
            }
            else if(PlayerPrefs.GetInt("timesToGo") == 0 && str == "video")
            {
                PlayerPrefs.SetInt("dailySpin", FunzioniUtili.GetDateValue("day"));
                PlayerPrefs.SetInt("timesToGo", 5);
                compraSkin(1);
                volte_premuto++;
                nVolte = 0;
                fuoriIF = dentroIF;
            }

        }
            
    }

    public void newButtonPressed() //azioni eseguite dopo aver premuto il pulsante invisibile
    {
        Destroy(Cavallo_da_spawnare);
        transform.localScale = new Vector3(327.9436f, 327.9436f, 327.9436f);
        Pulsante.SetActive(false);
        nomeCavallo.text = "";
        Rarita.text = "";
        testoConsolazione.SetActive(false);
        nuovoCavalloTesto.SetActive(false);
        exist = false;
    }
    
    public int getRarity(int index)
    {
        if(index<= 6)
        {
            return 0;
        } else if(index >=7 && index <= 11)
        {
            return 1;
        }
        else if (index >= 12 && index <= 14)
        {
            return 2;
        }
        else if (index >= 15 && index <= 16)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public void HorseUnlocker(int indice) //aggiunge il cavallo trovato tra quelli posseduti
    {
        newString = "";

        char[] unlockHorses = PlayerPrefs.GetString("unlockedHorses").ToCharArray();
        if(unlockHorses[indice] == '0')
        {
            unlockHorses[indice] = '1';
            //PlayerPrefs.SetString("unlockedHorses", unlockHorses.ToString());
            
            
          
            for (int i = 0; i < unlockHorses.Length; i++)
            {
                newString = newString + unlockHorses[i].ToString();
                //print(newString);
            }

            nuovoCavalloTesto.SetActive(true);

            PlayerPrefs.SetString("unlockedHorses", newString);


        } else if(unlockHorses[indice] == '1')
        {
            soldiConsolazione = soldiStandardConsolazione[getRarity(indice)];
            int cash = PlayerPrefs.GetInt("money") + soldiConsolazione;
            PlayerPrefs.SetInt("money", cash);
            //print("consolazione assegnata" + PlayerPrefs.GetInt("money"));
            testoConsolazione.GetComponent<Text>().text = string.Concat("OWNED +",soldiConsolazione);
            testoConsolazione.SetActive(true);
        }
    }

    public void spawnaCavallo(int index) //spawna il cavallo e lo sblocca
    {
        
        nomeCavallo.text = jsonReader.mySkinList.skinData[index].name;
        Rarita.text = jsonReader.mySkinList.skinData[index].rarity;

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Cavallo_da_spawnare = Instantiate(jsonReader.Horses[index], new Vector3(-24.59993f, 46.87132f, -90.64104f), Quaternion.Euler(new Vector3(50.35f, 0f, 0f)), Pulsante.transform );
        Vector3 dimensioniGrandi = Cavallo_da_spawnare.transform.localScale;
        Cavallo_da_spawnare.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Cavallo_da_spawnare.LeanScale(dimensioniGrandi, 0.3f);

        Pulsante.SetActive(true);
        HorseUnlocker(index);

        exist = true;
    }

    public void compraSkin(int i) //chiamata quando viene premuto il pulsante per comprare la skin
    {
        if(i == 0)
        {
            int soldi = PlayerPrefs.GetInt("money") - prezzo;
            PlayerPrefs.SetInt("money", soldi);

        }
        
        anim_macchinetta.Play("usa_macchinetta");
        StartTime = Time.time;
    }

    public int randomRarityGenerator()//genera casualmente un numero e ritorna la rarità corrispondente a quel numero
    {
        int i = Random.Range(1, 1000);
        if(i >= 1 && i< 400)
        {
            return 1;
        } 
        else if(i < 700 && i >= 400)
        {
            return 2;
        }
        else if (i <= 980 && i >= 700)
        {
            return 3;
        }
        else 
        {
            return 4;
        }
    }

    public static void Spin(GameObject oggettoh,float speed)
    {
        oggettoh.transform.Rotate(Vector3.up * speed * Time.fixedDeltaTime);
    }


    public int intMem;
    public int dentroIF;
    public int fuoriIF;
    public int nVolte;


    private void Update()
    {
        //NON TOCCARE QUESTA REGIONE PERCHE' FUNZIONA E NON SO NEMMENO IO COME
        #region Skin spawner
        jsonReader = GameObject.Find("SkinManager").GetComponent<Jsonreader>();
        testo_prezzo.text = prezzo.ToString();

        if(prezzo > PlayerPrefs.GetInt("money"))
        {
            testo_prezzo.color = new Color(0.794f, 0.01960784f, 0.01960784f);
        }
        else
        {
            testo_prezzo.color = new Color(0.5137255f, 0.2627451f, 0.1490196f);
        }

       

        if(Time.time - StartTime >= duration)
        {
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
        }
        
        if(!isPlaying && volte_premuto != 0)
        {
            
            
            if (intMem != volte_premuto)
            {
                
                
                intMem = volte_premuto;
                
                
            }
            dentroIF++;
        }
        fuoriIF++;
        if(dentroIF == 0)
        {
            fuoriIF = 0;
        }
        if(dentroIF != fuoriIF)
        {
            if(nVolte == 0)
            {
                int k = randomRarityGenerator();
                switch (k)
                {
                    case 1:
                        spawnaCavallo(Random.Range(1, 6));
                        break;
                    case 2:
                        spawnaCavallo(Random.Range(7, 11));
                        break;
                    case 3:
                        spawnaCavallo(Random.Range(12, 14));
                        break;
                    case 4:
                        spawnaCavallo(Random.Range(15, 16));
                        break;
                }
                nVolte=1;
            }

        }


        #endregion
        if (PlayerPrefs.GetInt("timesToGo") == 0)
        {
            buyButtonPress("video");
        }
            

        if (exist)
        {
            Cavallo_da_spawnare.transform.Rotate(Vector3.up * 15f * Time.fixedDeltaTime);
        }
        
        //print("playerpref: " + PlayerPrefs.GetString("unlockedHorses"));
    }

}

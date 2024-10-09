using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerLevels : MonoBehaviour
{
    [SerializeField]
    public GameObject[] tilePrefabs;

    [SerializeField]
    public GameObject[] PowerUps;

    [SerializeField]
    public GameObject[] Wheat;

    public float tileLength = 30;
    public float zSpawn;

    public int safeZone = 198;

    public int tiles_number;

    public int numberOftiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    public int probPowerUps;
    public int probWheat;

    public bool completed;
    public int[] powList;
    void Start()
    {
        powList = new int[] { 0, 0, 0, 0 };
        for(int j = 0; j< 4; j++){
            powList[j] = PlayerPrefs.GetInt(string.Concat("pow", (j + 1).ToString()));
            //print(string.Concat("pow", (j + 1).ToString())+ "    " + powList[j]);
        }

        completed = false;
        p1 = 0; p2 = 0; p3 = 0; p4 = 0;

        livello = PlayerPrefs.GetInt("level");
        //PlayerPrefs.GetInt("level")
        somma_peso = 5 + livello / 4;
        lunghezza = 10 + (livello / 3) * 2;

        if(somma_peso > 15)
        {
            somma_peso = 15;
        }

        Random.InitState(livello);



        //print("livello: " + livello + "lunghezza: " + lunghezza + "sommaPeso: " + somma_peso);
        pownumber = PlayerPrefs.GetInt("pow1") + PlayerPrefs.GetInt("pow2") + PlayerPrefs.GetInt("pow3") + PlayerPrefs.GetInt("pow4") ;
        //print(pownumber);
        probPowerUps = 69;
        probWheat = 250;
        numberOftiles = 10;
        tiles_number = 2 - numberOftiles;
        int i;
        zSpawn = 0 - tileLength;
        //genera casualmente la casella e la genera
        for (i = 0; i < numberOftiles; i++)
        {
            if (i <= 3)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(ExtractIndexValue(EstraiPeso()));
            }

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(tiles_number);
        if (playerTransform.position.z - safeZone > zSpawn - (numberOftiles * tileLength))
        {
            if (!completed)
            {
                if (tiles_number == lunghezza)
                {
                    SpawnTile(49);
                    completed = true;
                }
                else
                {
                    SpawnTile(ExtractIndexValue(EstraiPeso()));
                }
            }
            else
            {
                SpawnTile(0);
            }
            
            
            //SpawnTile(0);
            DeleteTile();
        }
        //print(" p1: " + p1 + " p2: " + p2 + " p3: " + p3 + " p4: " + p4 + " somma: "+ (p1+p2+p3+p4).ToString());
        //print("Peso:  " + EstraiPeso() + "  casella:  " + ExtractIndexValue());

    }

    private Quaternion rotation;
    int pownumber;
    public void SpawnTile(int tileIndex)
    {
        Vector3 tilePosition = transform.forward;
        tilePosition.z += zSpawn;
        GameObject go = Instantiate(tilePrefabs[tileIndex], tilePosition, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        //ySpawn -= 0.001f;

        
        
        if (tileIndex != 0)
        {

            SpawnWheat();
            SpawnPowerUps();
        }
        tiles_number++;

    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public bool GetRandomProbability(int intOutOf1000)
    {
        int n = Random.Range(1, 1000);
        if (n <= intOutOf1000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    public void SpawnPowerUps()
    {

        //print("entra 0");
        if (pownumber > 0)
        {
            //print("entra 1");
            if (GetRandomProbability(probPowerUps))
            {
                int index  = FunzioniUtili.RandomNumberFromList(powList);
                
                switch (index)
                {
                    case 0: //lattina
                        rotation = Quaternion.Euler(-90f, 0f, 0f);
                        break;
                    case 1: //scudo
                        rotation = Quaternion.Euler(-90f, 0f, 90f);
                        break;
                    case 2://stella
                        rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;
                    case 3://Magnete
                        rotation = Quaternion.Euler(0f, 0f, 90f);
                        break;


                }

                Instantiate(PowerUps[index],
                             new Vector3(Random.Range(-36, -6), 4.3f, zSpawn - Random.Range(0, tileLength)),
                             rotation);
                //print("powerup");
            }
        }
    }

    public void SpawnWheat()
    {
        if (GetRandomProbability(probWheat))
        {
            int index;
            if (Random.Range(1, 8) == 1)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }
            Instantiate(Wheat[index],
                             new Vector3(Random.Range(-36, -6), 4.1f, zSpawn - Random.Range(0, tileLength)),
                             Quaternion.Euler(0f, 0f, 0f));
        }
    }

    public int ExtractIndexValue(int peso)
    {
        int val;
        if( peso == 1)
        {
            val = Random.Range(0,12);
        }else if(peso == 2)
        {
            val = Random.Range(13, 24);
        }
        else if (peso == 3)
        {
            val = Random.Range(25, 36);
        }
        else 
        {
            val = Random.Range(37, 48);
        }

        return val;
    }


    
    int p1, p2, p3, p4;
    public int somma_peso;
    public int lunghezza;
    public int livello;
    private int tempSomma;
    public int EstraiPeso()
    {

        if(p1 == 0)
        {
            tempSomma = somma_peso;
            p1 = Random.Range(1, 4);
            if(p1 + 3 > tempSomma)
            {
                p1 = 1;
            }
            if(p1+12 < tempSomma)
            {
                p1 = 4;
            }
            tempSomma -= p1;
            p1 = ValueCheck(p1);
            return p1;
        } else if( p2 == 0)
        {
            p2 = tempSomma / 3;
            if(tempSomma%3 != 0 && p2 != 4)
            {
                p2++;
            }
            tempSomma -= p2;
            p2 = ValueCheck(p2);
            return p2;
        }else if (p3 == 0)
        {
            p3 = tempSomma / 2;
            if (tempSomma % 3 != 0 && p3 != 4)
            {
                p3++;
            }
            tempSomma -= p3;
            p3 = ValueCheck(p3);
            return p3;
        }else
        {
            p4 = tempSomma;
            p4 = ValueCheck(p4);
            int temp = p4;
            p1 = 0; p2 = 0; p3 = 0; p4 = 0;
            return temp;
        }

    }

    public int ValueCheck(int val)
    {
        if(val > 4)
        {
            return 4;
        } else if( val < 1)
        {
            return 1;
        }
        else
        {
            return val;
        }
    }
}

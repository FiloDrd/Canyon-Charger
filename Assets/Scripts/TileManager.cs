using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    
    [SerializeField]
    public GameObject[] tilePrefabs;

    [SerializeField]
    public GameObject[] PowerUps;

    [SerializeField]
    public GameObject[] Wheat;

    public float tileLength = 30;
    public float zSpawn;
    public static float ySpawn = 0;

    public int safeZone = 198;

    public int tiles_number;

    public int numberOftiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    public int probPowerUps;
    public int probWheat;
    void Start()
    {
        Random.InitState((int)(Time.time * Random.Range(0,7000)));
        pownumber = PlayerPrefs.GetInt("pow1") + PlayerPrefs.GetInt("pow2") + PlayerPrefs.GetInt("pow3") + PlayerPrefs.GetInt("pow4");
        probPowerUps = 59;
        probWheat = 300;
        numberOftiles = 10;
        tiles_number = 0 - numberOftiles;
        int i;
        zSpawn = 0 - tileLength;
        //genera casualmente la casella e la genera
        for (i = 0; i<numberOftiles ; i++)
        {
            if(i <= 3)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
            
            
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerTransform.position.z - safeZone> zSpawn -(numberOftiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            //SpawnTile(0);
            DeleteTile();
        }
    }

    private Quaternion rotation;
    int pownumber;

    public void SpawnTile(int tileIndex)
    {
        Vector3 tilePosition = transform.forward;
        tilePosition.z += zSpawn;
        tilePosition.y += ySpawn;
        GameObject go = Instantiate(tilePrefabs[tileIndex], tilePosition, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        //ySpawn -= 0.001f;
        tiles_number++;


        //spawna i powerups
        //SpawnPowerUps();

        //spawna il grano
        SpawnWheat();

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public bool GetRandomProbability( int intOutOf1000)
    {
        int n = Random.Range(1,1000);
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
       /* if (pownumber > 0)
        {
            if (GetRandomProbability(probPowerUps))
            {
                int index;
                bool existPow = false;
                do
                {
                    index = Random.Range(0, PowerUps.Length - 1);
                    if (PlayerPrefs.GetInt("pow" + (index + 1).ToString()) != 0)
                    {
                        existPow = true;
                    }

                } while (existPow);

                switch (index)
                {
                    case 0:
                        rotation = Quaternion.Euler(-90f, 0f, 0f);
                        break;
                    case 1:
                        rotation = Quaternion.Euler(0f, 0f, 90f);
                        break;
                    case 2:
                        rotation = Quaternion.Euler(-90f, 0f, 0f);
                        break;
                    case 3:
                        rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;


                }

                Instantiate(PowerUps[index],
                             new Vector3(Random.Range(-36, -6), 4.3f, zSpawn - Random.Range(0, tileLength)),
                             rotation);
            }
        }*/
    }

    public void SpawnWheat()
    {
        if (GetRandomProbability(probWheat))
        {
            int index;
            if(Random.Range(1, 6) == 1)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }
            Instantiate(Wheat[index],
                             new Vector3(Random.Range(-36, -6), 4.1f, zSpawn - Random.Range(0, tileLength)),
                             Quaternion.Euler(0f,0f,0f));
        }
    }
}

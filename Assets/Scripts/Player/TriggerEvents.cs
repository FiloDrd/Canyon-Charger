using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerEvents : MonoBehaviour
{


    public GameObject Shield;
    public GameObject Magnete;
    public GameObject Stella;
    public GameObject Lattina;

    public Animator cavalloAnimator;
    public Animator deathAnimator;

    //public ParticleSystem DestroyParticles;
    public ParticleSystem PowerUpParticles;
    public ParticleSystem CoinParticles;
    public GameObject GameOverPanel;
    public Text ScoreText;
    public Text Coin;
    public Text DeathScore;
    public Text DeathCoin;

    private float TimeMagnete;
    private float MagneteMultiplier;
    private float TimeShield;
    private float ShieldMultiplier;
    private float TimeStar;
    private float StarMultiplier;
    private float TimeLattina;
    private float LattinaMultiplier;

    [HideInInspector]public static bool isShieldActive;
    private bool isStarActive;
    private bool isMagnetActive;
    private bool isLattinaActive;

    public static int coins;
    public static float score;

    //Timers
    private float startTime1;
    private float startTime2;
    private float startTime3;
    private float startTime4;

    public Text Timer1;
    public Text Timer2;
    public Text Timer3;
    public Text Timer4;

    private void Start()
    {
        #region Costruttore
        isStarActive = false;
        isShieldActive = false;
        isMagnetActive = false;
        coins = 0;
        score = 0;

        ShieldMultiplier = 3f;
        MagneteMultiplier = 3f;
        StarMultiplier = 3f;
        LattinaMultiplier = 3f;
        TimeMagnete = MagneteMultiplier * PlayerPrefs.GetInt("pow4");
        TimeShield = ShieldMultiplier * PlayerPrefs.GetInt("pow2");
        TimeStar = StarMultiplier * PlayerPrefs.GetInt("pow3");
        TimeLattina = LattinaMultiplier * PlayerPrefs.GetInt("pow1");
        #endregion

        deathAnimator = GameObject.FindGameObjectWithTag("G2").GetComponent<Animator>();
        cavalloAnimator = GameObject.FindGameObjectWithTag("GraficaCavallo").GetComponent<Animator>();

        PlayerPrefs.SetInt("isCompleted", FunzioniUtili.BoolToInt(false));
        PlayerPrefs.SetInt("isMagnetActive", FunzioniUtili.BoolToInt(isMagnetActive));
        PlayerPrefs.SetInt("isLattinaActive", FunzioniUtili.BoolToInt(isLattinaActive));
        PlayerPrefs.SetInt("isGameOver", FunzioniUtili.BoolToInt(false));
        CoinParticles.Stop();
        PowerUpParticles.Stop();
        //DestroyParticles.Stop();
    }

    private void Update()
    {
        //print("Magnete: " + isMagnetActive + "---- Lattina: " + isLattinaActive + "---- Stella: " + isStarActive + "---- Scudo: " + isShieldActive); 
        UpdateScore();
        Coin.text = coins.ToString();
        ScoreText.text = score.ToString("0");

        //Timer1.text = (     System.Math.Round(   TimeMagnete - (Time.time - startTime)    )    ).ToString();
        TimerTextChanger(Timer1, startTime1);
        TimerTextChanger(Timer2, startTime2);
        TimerTextChanger(Timer3, startTime3);
        TimerTextChanger(Timer4, startTime4);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fieno5") || other.CompareTag("fieno"))
        {
            CoinParticles.Play();
            if(other.tag == "fieno")
            {
                coins += 1 * GetStarMultiplier();
            }
            if (other.tag == "fieno5")
            {
                coins += 5 * GetStarMultiplier();
            }
        }

        if (other.CompareTag("Obstacle"))
        {
            
            if (!isShieldActive)
            {
                print("GameOver");
                PlayerPrefs.SetInt("isGameOver", FunzioniUtili.BoolToInt(true));
                StartCoroutine(TriggerDeath(2f));
            }
            else
            {
                //DestroyParticles.gameObject.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                //DestroyParticles.Play();
                StopCoroutine(ShieldActivator(TimeShield));
                isShieldActive = false;
                Shield.SetActive(false);
            }
        }

        if (other.CompareTag("Shield"))
        {
            PowerUpParticles.Play();
            if (isShieldActive)
            {
                StopCoroutine(ShieldActivator(TimeShield));
                isShieldActive = false;
                Shield.SetActive(false);
            }
            StartCoroutine(ShieldActivator(TimeShield));
        }

        if (other.CompareTag("magnete"))
        {
            PowerUpParticles.Play();
            if (isMagnetActive)
            {
                StopCoroutine(MagnetActivator(TimeMagnete));
                isMagnetActive = false;
                PlayerPrefs.SetInt("isMagnetActive", FunzioniUtili.BoolToInt(isMagnetActive));
                Magnete.SetActive(false);
            }
            StartCoroutine(MagnetActivator(TimeMagnete));
        }

        if (other.CompareTag("Star"))
        {
            PowerUpParticles.Play();
            if (isStarActive)
            {
                StopCoroutine(StarActivator(TimeStar));
                isStarActive = false;
                Stella.SetActive(false);
            }
            StartCoroutine(StarActivator(TimeStar));
        }

        if (other.CompareTag("Lattina"))
        {
            PowerUpParticles.Play();
            if (isLattinaActive)
            {
                StopCoroutine(LattinaActivator(TimeLattina));
                isLattinaActive = false;
                PlayerPrefs.SetInt("isLattinaActive", FunzioniUtili.BoolToInt(isLattinaActive));
                Lattina.SetActive(false);
            }
            StartCoroutine(LattinaActivator(TimeLattina));
        }

    }

    #region Coroutines 
    IEnumerator TriggerDeath(float duration)
    {
        cavalloAnimator.Play("Stop");
        cavalloAnimator.enabled = false;
        deathAnimator.enabled = true;
        yield return new WaitForSeconds(duration);
        int lastHighScore = PlayerPrefs.GetInt("highscore");
        if(score > lastHighScore)
        {
            int newHighScore = (int)score;
            PlayerPrefs.SetInt("highscore", newHighScore);
        }
        ActivateGameOver();


    }

    IEnumerator MagnetActivator(float duration)
    {
        Magnete.transform.position = new Vector3(GetXPosition(), Magnete.transform.position.y, Magnete.transform.position.z);
        isMagnetActive = true;
        Magnete.SetActive(true);
        PlayerPrefs.SetInt("isMagnetActive", FunzioniUtili.BoolToInt(isMagnetActive));
        startTime1 = Time.time;
        yield return new WaitForSeconds(duration);
        isMagnetActive = false;
        Magnete.SetActive(false);
        PlayerPrefs.SetInt("isMagnetActive", FunzioniUtili.BoolToInt(isMagnetActive));
    }

    IEnumerator ShieldActivator(float duration)
    {
        Shield.transform.position = new Vector3(GetXPosition(), Shield.transform.position.y, Shield.transform.position.z);
        Shield.SetActive(true);
        isShieldActive = true;
        startTime2 = Time.time;
        yield return new WaitForSeconds(duration);
        Shield.SetActive(false);
        isShieldActive = false;
    }

    IEnumerator StarActivator(float duration)
    {
        Stella.transform.position = new Vector3(GetXPosition(), Stella.transform.position.y, Stella.transform.position.z);
        Stella.SetActive(true);
        isStarActive = true;
        startTime3 = Time.time;
        yield return new WaitForSeconds(duration);
        Stella.SetActive(false);
        isStarActive = false;
    }

    IEnumerator LattinaActivator(float duration)
    {
        Lattina.transform.position = new Vector3(GetXPosition(), Lattina.transform.position.y, Lattina.transform.position.z);
        isLattinaActive = true;
        Lattina.SetActive(true);
        PlayerPrefs.SetInt("isLattinaActive", FunzioniUtili.BoolToInt(isLattinaActive));
        startTime4 = Time.time;
        yield return new WaitForSeconds(duration);
        isLattinaActive = false;
        Lattina.SetActive(false);
        PlayerPrefs.SetInt("isLattinaActive", FunzioniUtili.BoolToInt(isLattinaActive));

    }
    #endregion

    #region AltreFunzioni

    public void AnimatorManager(Animator animator, float startTime)
    {
        if((Time.time - startTime)< 3f)
        {
            animator.Play("ExtremeYetEvenFasterCasellaLampeggia");
        } else if((Time.time - startTime) < 10f)
        {
            animator.Play("YetEvenFasterCasellaLampeggia");
        }
        else if ((Time.time - startTime) < 15f)
        {
            animator.Play("EvenFasterCasellaLampeggia");
        }
        else if ((Time.time - startTime) < 20f)
        {
            animator.Play("FasterCasellaLampeggia");
        }
        else if ((Time.time - startTime) < 30f)
        {
            animator.Play("CasellaLampeggia");
        }
    }

    public void TimerTextChanger(Text txt, float startTime)
    {
        
            txt.text = (System.Math.Round(TimeMagnete - (Time.time - startTime))).ToString();

        if (int.Parse(txt.text) < 0)
        {
            txt.text = "0";
        } 

    }

    public void ActivateGameOver()
    {
        Time.timeScale = 0f;
        DeathScore.text = score.ToString("0");
        DeathCoin.text = coins.ToString();
        GameOverPanel.SetActive(true);

    }

    private float delta_position;
    private float lastPosition;
    public void UpdateScore()
    {
        delta_position = (transform.position.z - lastPosition);
        lastPosition = transform.position.z;
        score += delta_position * GetStarMultiplier();

    }

    public int NumberActivePowerups()
    {
        int n = 0;
        if (isLattinaActive) { n++; }
        if (isMagnetActive) { n++; }
        if (isShieldActive) { n++;  }
        if (isStarActive) { n++;  }

        return n-1;
    }
    
    public float GetXPosition()
    {
        float Delta = 26.2f - 23.9f;
        float pos = 0f - 23.9f + (Delta * NumberActivePowerups());
        return pos;
    }

    public int GetStarMultiplier()
    {
        if (isStarActive)
        {
            return 2;
        } 
        else
        {
            return 1;
        }
    }

    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        int tempCoins = PlayerPrefs.GetInt("money") + coins;
        PlayerPrefs.SetInt("money", tempCoins);
        int lev = PlayerPrefs.GetInt("level") + 1;
        PlayerPrefs.SetInt("level", lev);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryButton()
    {
        Time.timeScale = 1f;
        int tempCoins = PlayerPrefs.GetInt("money") + coins;
        PlayerPrefs.SetInt("money", tempCoins);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        int tempCoins = PlayerPrefs.GetInt("money") + coins;
        PlayerPrefs.SetInt("money", tempCoins);
        SceneManager.LoadScene("StartMenu");
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using NUnit.Framework;
using UnityEngine.SocialPlatforms;

public class GenerationEngine : MonoBehaviour {

    public GameObject PlayerOrb;
    public GameObject ScoreOrb;
    public GameObject DeathOrb;
    public GameObject BonusOrb;
    public GameObject BulletOrb;
    public float spawnRate = 1;
    public Slider explosionSlider;
    public float decreaseTimeRate = 0.5f;
    public float decreaseValueRate = 0.1f;
    public TextMeshProUGUI scoreText;
    public AudioSource ISeeYouSource;

    private int deathOrbSpawnRate = 1;
    private int bulletOrbSpawnRate = 0;
    [HideInInspector]
    public int score = 0;
    private int capScore = 500;
    private bool firstCap = false;
    private bool stopSpawnDeathOrbs = false;
    private int capScoreBullets = 1000;
    private bool stopSpawnBulletOrbs = false;
    private List<GameObject> bonusOrbs;
    private List<GameObject> deathOrbs;

    [HideInInspector]
    public static float borderX = 2.5f;
    [HideInInspector]
    public static float borderY = 4f;
    
    //Achievments
    private const string ach1 = "CgkIx5CvwKwMEAIQAA";
    private const string ach2 = "CgkIx5CvwKwMEAIQAQ";
    private const string ach3 = "CgkIx5CvwKwMEAIQAg";
    private const string ach4 = "CgkIx5CvwKwMEAIQAw";
    private const string ach5 = "CgkIx5CvwKwMEAIQBA";
    
	// Use this for initialization
	void Start ()
	{
	    PlayGamesPlatform.Activate();
	    Social.localUser.Authenticate((bool success) =>
	    {
	        if(success) print("Okay login");
	    });
	    
	    Time.timeScale = 1f;
	    var randomSound = Random.Range(1, 3);
	    if (randomSound == 2)
	    {
	        ISeeYouSource.Play();
	    }

	    if (PlayerPrefs.HasKey("CoinRestart"))
	    {
	        if (PlayerPrefs.GetString("CoinRestart") == "true")
	        {
	            score = PlayerPrefs.GetInt("Score");
	            scoreText.SetText("Score : "+ score);
	            PlayerPrefs.SetString("CoinRestart", "false");
	        }
	    }
        
        if (PlayerPrefs.HasKey("ad"))
        {
            if (PlayerPrefs.GetString("ad") == "true")
            {
                score = PlayerPrefs.GetInt("Score");
                scoreText.SetText("Score : "+ score);
                PlayerPrefs.SetString("ad","used");
            }
        }
        
        deathOrbs = new List<GameObject>();
        bonusOrbs = new List<GameObject>();
        SpawnOrbs();
        StartCoroutine(DecreaseSliderValue());
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(explosionSlider.value <= 0)
        {
            Death();
        }
	}

    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id,100f, (bool success) =>
        {
            if(success) print("Okay login");
        });
    }

    public void SpawnBulletOrb()
    {
        GameObject obj = Instantiate(BulletOrb) as GameObject;
        BulletOrb valObj = obj.GetComponent<BulletOrb>();
        
        Vector3 spawnPosition = new Vector3(5,5,5);
        
        switch (Random.Range(1,5))
        {
            case 1:
                spawnPosition = new Vector3(Random.Range(-2.5f,2.5f),4f,obj.transform.position.z);
                obj.transform.SetPositionAndRotation(spawnPosition, Quaternion.Euler(0,0,-90));
                valObj.state = global::BulletOrb.StatesOfMovement.Down;
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-2.5f,2.5f),-4f,obj.transform.position.z);
                obj.transform.SetPositionAndRotation(spawnPosition, Quaternion.Euler(0,0,90));
                valObj.state = global::BulletOrb.StatesOfMovement.Up;
                break;
            case 3:
                spawnPosition = new Vector3(2.5f,Random.Range(-4f,4f),obj.transform.position.z);
                obj.GetComponent<SpriteRenderer>().flipY = true;
                obj.transform.SetPositionAndRotation(spawnPosition, Quaternion.Euler(0,0,-180));
                valObj.state = global::BulletOrb.StatesOfMovement.Left;
                break;
            case 4:
                spawnPosition = new Vector3(-2.5f,Random.Range(-4f,4f),obj.transform.position.z);
                obj.transform.position = spawnPosition;
                valObj.state = global::BulletOrb.StatesOfMovement.Right;
                break;	      
        }     
        
    }

    public void SpawnOrbs()
    {
       
        ClearOrbs();
        ValidateSpawnRateOfDeathOrbs();
        ValidateSpawnRateOfBulletOrbs();
        explosionSlider.value += 0.5f;

        deathOrbs.Clear();
        for(int i = 0; i < deathOrbSpawnRate; ++i)
        {

            float deathX = Random.Range(-borderX, borderX);
            float deathY = Random.Range(-borderY, borderY);
            ValidateDeathOrbPosition(ref deathX, ref deathY);
            deathOrbs.Add(Instantiate(DeathOrb, new Vector3(deathX, deathY, 0), DeathOrb.transform.rotation));
        }

        for (int i = 0; i < bulletOrbSpawnRate; ++i)
        {
            SpawnBulletOrb();   
        }

        float scoreX = Random.Range(-borderX, borderX);
        float scoreY = Random.Range(-borderY, borderY);
        ValidateScoreOrbPosition(ref scoreX, ref scoreY);

        float bonusX = Random.Range(-borderX, borderX);
        float bonusY = Random.Range(-borderY, borderY);

        Instantiate(ScoreOrb, new Vector3(scoreX, scoreY, 0), ScoreOrb.transform.rotation);
        bonusOrbs.Add(Instantiate(BonusOrb, new Vector3(bonusX, bonusY, 0), BonusOrb.transform.rotation));

    }

    private void ValidateSpawnRateOfBulletOrbs()
    {
        if (!stopSpawnBulletOrbs)
        {
            if (score >= capScoreBullets)
            {
                ++bulletOrbSpawnRate;
                capScoreBullets += 1500;
            }
            if (capScoreBullets == 4000)
            {
                capScoreBullets += 1000;
            }
            if (capScoreBullets >= 6500 && !stopSpawnBulletOrbs)
            {
                stopSpawnBulletOrbs = true;
            }
        }
    }
    
    private void ValidateSpawnRateOfDeathOrbs()
    {
        if (!stopSpawnDeathOrbs)
        {
            if (score >= capScore && !firstCap)
            {
                ++deathOrbSpawnRate;
                capScore += 500;
                firstCap = true;
            }
            else if (score >= capScore)
            {
                ++deathOrbSpawnRate;
                capScore += 1000;
            }
            if (deathOrbSpawnRate == 4)
            {
                stopSpawnDeathOrbs = true;
            }
        }
        if (score > 7250)
        {
            ++deathOrbSpawnRate;
        }
    }

    private void ValidateScoreOrbPosition(ref float x, ref float y)
    {
        foreach(GameObject item in deathOrbs)
        {
            if (x > 0 && y > 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x -= Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
                {
                    y -= Random.Range(1.5f, 3f);
                }
            }
            else if (x < 0 && y < 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x += Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y += Random.Range(1.5f, 3f);
                }
            }
            else if (x < 0 && y > 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x += Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y -= Random.Range(1.5f, 3f);
                }
            }
            else if (x > 0 && y < 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x -= Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y += Random.Range(1.5f, 3f);
                }
            }
        }
    }

    private void ValidateBonusOrbPosition(ref float x, ref float y)
    {
        foreach (GameObject item in deathOrbs)
        {
            if (x > 0 && y > 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x -= Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
                {
                    y -= Random.Range(1.5f, 3f);
                }
                break;
            }
            else if (x < 0 && y < 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x += Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y += Random.Range(1.5f, 3f);
                }
                break;
            }
            else if (x < 0 && y > 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x += Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y -= Random.Range(1.5f, 3f);
                }
                break;
            }
            else if (x > 0 && y < 0)
            {
                if (System.Math.Abs(item.transform.position.x - x) < 0.8f)
                {
                    x -= Random.Range(0.3f, 1f);
                }
                if (System.Math.Abs(item.transform.position.y - y) < 1f)
                {
                    y += Random.Range(1.5f, 3f);
                }
                break;
            }
        }
    }

    private void ValidateDeathOrbPosition(ref float x, ref float y)
    {
        if(x >0 && y > 0)
        {
            if(System.Math.Abs(PlayerOrb.transform.position.x - x) < 0.8f)
            {
                x -= Random.Range(0.3f, 1f);
            }
            if(System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
            {
                y -= Random.Range(1.5f, 3f);
            }
        }
        else if(x < 0 && y < 0)
        {
            if (System.Math.Abs(PlayerOrb.transform.position.x - x) < 0.8f)
            {
                x += Random.Range(0.3f, 1f);
            }
            if (System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
            {
                y += Random.Range(1.5f, 3f);
            }
        }
        else if (x < 0 && y > 0)
        {
            if (System.Math.Abs(PlayerOrb.transform.position.x - x) < 0.8f)
            {
                x += Random.Range(0.3f, 1f);
            }
            if (System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
            {
                y -= Random.Range(1.5f, 3f);
            }
        }
        else if (x > 0 && y < 0)
        {
            if (System.Math.Abs(PlayerOrb.transform.position.x - x) < 0.8f)
            {
                x -= Random.Range(0.3f, 1f);
            }
            if (System.Math.Abs(PlayerOrb.transform.position.y - y) < 1f)
            {
                y += Random.Range(1.5f, 3f);
            }
        }
    }

    IEnumerator DecreaseSliderValue()
    {
        while(explosionSlider.value >= 0)
        {
            explosionSlider.value -= decreaseValueRate;
            yield return new WaitForSeconds(decreaseTimeRate);
        }
    }

    public void PlusScore(int sc)
    {
        score += sc;
        if (score >= 6000)
        {
            GetTheAchiv(ach3);
        }
        if (score >= 1000)
        {
            GetTheAchiv(ach4);
        }
        scoreText.SetText("Score : "+ score);
    }

    public void Death()
    {
        if (PlayerPrefs.HasKey("CountDeath"))
        {
            var countDeaths = PlayerPrefs.GetInt("CountDeath");
            ++countDeaths;
            if (countDeaths >= 500)
            {
                GetTheAchiv(ach1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CountDeath",1);
            GetTheAchiv(ach5);
        }
        
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("DeathScene");
    }

    public void AddCoin()
    {
      var coins = PlayerPrefs.GetInt("Coins");
        ++coins;
        if (coins >= 100)
        {
            GetTheAchiv(ach2);
        }
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void ClearOrbs()
    {
        foreach(GameObject item in deathOrbs)
        {
            Destroy(item);
        }

        foreach(GameObject item in bonusOrbs)
        {
            Destroy(item);
        }
    }

}

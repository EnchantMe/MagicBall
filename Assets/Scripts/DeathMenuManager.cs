using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DeathMenuManager : MonoBehaviour {

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public AudioSource CrazyLaugh;

	// Use this for initialization
	void Start ()
	{
	    var randomSound = Random.Range(1, 3);
	    if (randomSound == 2)
	    {
	        CrazyLaugh.Play();   
	    }
	    if (!PlayerPrefs.HasKey("deathAd"))
	    {
	        PlayerPrefs.SetInt("deathAd", 1);
	    }
	    else
	    {
	        int deathAd = PlayerPrefs.GetInt("deathAd");
	        ++deathAd;
	        PlayerPrefs.SetInt("deathAd", deathAd);
	        if (deathAd >= 5)
	        {
	            PlayerPrefs.SetInt("deathAd", 1);
	            RequestInterstitialAd();
	        }
	    }

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        int currentScore = PlayerPrefs.GetInt("Score");
        int highScore = PlayerPrefs.GetInt("HighScore");

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }

        currentScoreText.SetText("Score: " + PlayerPrefs.GetInt("Score"));
        highScoreText.SetText("High score: " + PlayerPrefs.GetInt("HighScore"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RequestInterstitialAd()
    {
     #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5049056985743619/8282846879";
     #else
        string adUnitId = "unexpected_platform";
     #endif

        // Initialize an InterstitialAd.
        InterstitialAd interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
        
    }
    
    public void RequestRewardBasedVideo()
    {
        if (PlayerPrefs.HasKey("ad"))
        {
            if (PlayerPrefs.GetString("ad") == "used")
            {
                PlayerPrefs.DeleteKey("ad");
                Event_Restart();
            }
        }
        
        #if UNITY_EDITOR
        string rewardVideo = "unused";
        #elif UNITY_ANDROID
        string rewardVideo = "ca-app-pub-5049056985743619/7104690675";
        #endif
        
        RewardBasedVideoAd rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, rewardVideo);

        rewardBasedVideo.OnAdRewarded += OnRewardBasedVideoOnOnAdRewarded;
        
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    private void OnRewardBasedVideoOnOnAdClosed(object sender, EventArgs args)
    {
        Event_AdRestart();
    }

    private void OnRewardBasedVideoOnOnAdRewarded(object sender, Reward reward)
    {
        Event_AdRestart();
    }

    private void Event_AdRestart()
    {
        PlayerPrefs.SetString("ad", "true");
        SceneManager.LoadScene("PlayScene");
    }

    public void Event_Restart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Event_CoinsRestart()
    {
        var coins = PlayerPrefs.GetInt("Coins");

        if (!(coins - 50 < 0))
        {
            coins -= 50;
            PlayerPrefs.SetString("CoinRestart","true");
            PlayerPrefs.SetInt("Coins", coins);
            Event_Restart();
        }
    }

    public void Event_Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

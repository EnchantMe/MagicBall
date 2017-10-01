using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour {

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

	// Use this for initialization
	void Start () {

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

    public void Event_Restart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Event_Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

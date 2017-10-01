using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour {

    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highScore;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }

        currentScore.SetText("Score: " + PlayerPrefs.GetInt("Score"));
        highScore.SetText("High score: " + PlayerPrefs.GetInt("HighScore"));
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

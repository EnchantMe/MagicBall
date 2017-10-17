using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	public TextMeshProUGUI CoinsText;
	public Image SoundImage;
	public Sprite EnableSound;
	public Sprite DisableSound;
	private bool SoundEnable = true;
	private int Coins;
	
	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.HasKey("Sound"))
		{
			if (PlayerPrefs.GetString("Sound") == "false")
			{
				DisableProjectSound();
			}
			else
			{
				EnableProjectSound();
			}
		}
		if (!PlayerPrefs.HasKey("Coins"))
		{
			PlayerPrefs.SetInt("Coins", 0);
		}
		Coins = PlayerPrefs.GetInt("Coins");
		CoinsText.SetText(Coins.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Event_Exit()
    {
        Application.Quit();
    }

    public void Event_Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void Event_Play()
    {
        SceneManager.LoadScene("PlayScene");
    }

	public void Event_Sound()
	{
		if (SoundEnable)
		{
		   DisableProjectSound();
		}
		else
		{
		   EnableProjectSound();
		}
	}

	private void DisableProjectSound()
	{
		PlayerPrefs.SetString("Sound","false");
		AudioListener.volume = 0;
		SoundEnable = false;
		SoundImage.sprite = DisableSound;
	}

	private void EnableProjectSound()
	{
		PlayerPrefs.SetString("Sound","true");
		SoundImage.sprite = EnableSound;
		AudioListener.volume = 1;
		SoundEnable = true;
	}
}

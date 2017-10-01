using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
}

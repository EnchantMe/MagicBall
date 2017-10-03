using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    public Canvas mainCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Event_Pause()
    {
        mainCanvas.sortingOrder = 2;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Event_Resume()
    {
        mainCanvas.sortingOrder = 1;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Event_Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

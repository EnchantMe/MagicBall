using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    public Canvas mainCanvas;

    private bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Event_Pause();
            }
            else
            {
                Event_Resume();
            }
        }
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

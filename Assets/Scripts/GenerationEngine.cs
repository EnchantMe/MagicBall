using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GenerationEngine : MonoBehaviour {

    public GameObject Orb;
    public float spawnRate = 1;
    public Slider explosionSlider;
    public float decreaseTimeRate = 0.5f;
    public float decreaseValueRate = 0.1f;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    
	// Use this for initialization
	void Start ()
    {
        SpawnOrb();
        StartCoroutine(DecreaseSliderValue());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(explosionSlider.value <= 0)
        {
            //explosion stuff
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("DeathScene");
        }
	}

    public void SpawnOrb()
    {
        explosionSlider.value += 0.5f;

        float x = Random.Range(-2.5f, 2.5f);
        float y = Random.Range(-4f, 4f);

        Instantiate(Orb, new Vector3(x, y, 0), Orb.transform.rotation);
       
    }

    IEnumerator DecreaseSliderValue()
    {
        while(explosionSlider.value >= 0)
        {
            explosionSlider.value -= decreaseValueRate;
            yield return new WaitForSeconds(decreaseTimeRate);
        }
    }

    public void PlusScore()
    {
        score += 10;
        scoreText.SetText("Score : "+ score);
    }

}

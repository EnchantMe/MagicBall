using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GenerationEngine : MonoBehaviour {

    public GameObject ScoreOrb;
    public GameObject DeathOrb;
    public float spawnRate = 1;
    public Slider explosionSlider;
    public float decreaseTimeRate = 0.5f;
    public float decreaseValueRate = 0.1f;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private List<GameObject> deathOrbs;
    
	// Use this for initialization
	void Start ()
    {
        deathOrbs = new List<GameObject>();
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

    public void SpawnOrbs()
    {
        ClearDeathOrbs();
        explosionSlider.value += 0.5f;

        float scoreX = Random.Range(-2.5f, 2.5f);
        float scoreY = Random.Range(-4f, 4f);

        float deathX = Random.Range(-2.5f, 2.5f);
        float deathY = Random.Range(-4f, 4f);

        Instantiate(ScoreOrb, new Vector3(scoreX, scoreY, 0), ScoreOrb.transform.rotation);
        deathOrbs.Add(Instantiate(DeathOrb, new Vector3(deathX, deathY, 0), DeathOrb.transform.rotation));
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

    public void Death()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("DeathScene");
    }

    public void ClearDeathOrbs()
    {
        foreach(GameObject item in deathOrbs)
        {
            Destroy(item);
        }
    }

}

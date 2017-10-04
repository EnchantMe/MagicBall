using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GenerationEngine : MonoBehaviour {

    public GameObject PlayerOrb;
    public GameObject ScoreOrb;
    public GameObject DeathOrb;
    public GameObject BonusOrb;
    public float spawnRate = 1;
    public Slider explosionSlider;
    public float decreaseTimeRate = 0.5f;
    public float decreaseValueRate = 0.1f;
    public TextMeshProUGUI scoreText;

    private int deathOrbSpawnRate = 1;
    private int score = 0;
    private List<GameObject> bonusOrbs;
    private List<GameObject> deathOrbs;

    private float borderX = 2.5f;
    private float borderY = 4f;
    
	// Use this for initialization
	void Start ()
    {
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

    public void SpawnOrbs()
    {
        if (score >= 500)
        {
            deathOrbSpawnRate = 2;
        }
        if (score >= 1500)
        {
            deathOrbSpawnRate = 3;
        }

        ClearOrbs();
        explosionSlider.value += 0.5f;

        deathOrbs.Clear();
        for(int i = 0; i < deathOrbSpawnRate; ++i)
        {

            float deathX = Random.Range(-borderX, borderX);
            float deathY = Random.Range(-borderY, borderY);
            ValidateDeathOrbPosition(ref deathX, ref deathY);
            deathOrbs.Add(Instantiate(DeathOrb, new Vector3(deathX, deathY, 0), DeathOrb.transform.rotation));
        }

        float scoreX = Random.Range(-borderX, borderX);
        float scoreY = Random.Range(-borderY, borderY);
        ValidateScoreOrbPosition(ref scoreX, ref scoreY);

        float bonusX = Random.Range(-borderX, borderX);
        float bonusY = Random.Range(-borderY, borderY);

        Instantiate(ScoreOrb, new Vector3(scoreX, scoreY, 0), ScoreOrb.transform.rotation);
        bonusOrbs.Add(Instantiate(BonusOrb, new Vector3(bonusX, bonusY, 0), BonusOrb.transform.rotation));

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
        scoreText.SetText("Score : "+ score);
    }

    public void Death()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("DeathScene");
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

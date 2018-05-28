using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public GameManager gameManager;
    public BloodManager bloodManager;

    
    public TextMeshProUGUI highScoreText;

    public float levelScore;
    public float totalScore;

    public float deathCount;
    public float totalDeathCount;

    public float levelBloodLoss;
    public float totalBloodLoss;

    public float levelTime;
    public float totalTime;

    void Awake()
    {
        if (!this.gameObject)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void UpdateScore()
    {
        ScoreCalculator();

        Debug.Log("LevelScore: "+ levelScore);
        Debug.Log("DeathCount: " + deathCount);
        Debug.Log("LevelBloodLoss: " + levelBloodLoss);

        deathCount = PlayerPrefs.GetFloat("DeathCount");
        totalScore += levelScore;
        totalDeathCount += deathCount;
        totalBloodLoss += levelBloodLoss;

        PlayerPrefs.SetFloat("TotalDeathCount", totalDeathCount += PlayerPrefs.GetFloat("DeathCount"));
        PlayerPrefs.SetFloat("TotalBloodLoss", totalBloodLoss += PlayerPrefs.GetFloat("LevelBloodLoss"));
        PlayerPrefs.SetFloat("TotalScore", totalScore += PlayerPrefs.GetFloat("LevelScore"));
        Debug.Log("TotalDeathCount: " + totalDeathCount);
        Debug.Log("TotalBloodLoss: " + totalBloodLoss);
        Debug.Log("TotalScore: " + totalScore);
        
        if (SceneManager.GetActiveScene().name=="HighScore")
        {
            if (totalScore > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.GetFloat("HighScore", totalScore);
                highScoreText.text = totalScore.ToString();
                PlayerPrefs.SetFloat("HighScore", totalScore);
            }
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString(); ;
    }

    public void ScoreCalculator()
    {
        Debug.Log("Update score.");
        levelTime = gameManager.timeLeft;
        levelBloodLoss = 50 - bloodManager.bloodLevel;

        levelScore = Mathf.Pow(10, Mathf.Round(bloodManager.bloodLevel - PlayerPrefs.GetFloat("DeathCount")) / 100f) * levelTime;
        PlayerPrefs.SetFloat("LevelScore", levelScore);
    }
}

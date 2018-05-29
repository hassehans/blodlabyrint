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

        totalScore = PlayerPrefs.GetFloat("TotalScore");
        totalScore += levelScore;
        PlayerPrefs.SetFloat("TotalScore", totalScore);

        totalTime = PlayerPrefs.GetFloat("TotalTime");
        totalTime += levelTime;
        PlayerPrefs.SetFloat("TotalTime", totalTime);

        totalDeathCount = PlayerPrefs.GetFloat("TotalDeathCount");
        totalDeathCount += deathCount;
        PlayerPrefs.SetFloat("TotalDeathCount", totalDeathCount);

        totalBloodLoss = PlayerPrefs.GetFloat("TotalBloodLoss");
        totalBloodLoss += levelBloodLoss;
        PlayerPrefs.SetFloat("TotalBloodLoss", totalBloodLoss);
        
        Debug.Log("TotalDeathCount: " + totalDeathCount);
        Debug.Log("TotalBloodLoss: " + totalBloodLoss);
        Debug.Log("TotalScore: " + totalScore);
    }

    public void ScoreCalculator()
    {
        Debug.Log("Update score.");
        levelTime = gameManager.finishTime;
        deathCount = PlayerPrefs.GetFloat("DeathCount");

        levelScore = Mathf.Pow(10, Mathf.Round(levelBloodLoss - deathCount) / 100f) * levelTime;
        PlayerPrefs.SetFloat("LevelScore", levelScore);
    }
}

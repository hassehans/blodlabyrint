﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    public static bool gameIsPaused = false;

    public GameManager gameManager;
    public ScoreManager scoreManager;
    public BloodManager bloodManager;
    public Highscores highscoreManager;
    public GameObject levelCompleteWrapper;
    public GameObject nextlevelButton;
    public GameObject UICanvas;

    public TextMeshProUGUI statsTitle;
    public TextMeshProUGUI statsTimeLeft;
    public TextMeshProUGUI statsDeaths;
    public TextMeshProUGUI statsBlood;
    public TextMeshProUGUI statsScore;

    public TextMeshProUGUI totalStatsTitle;
    public TextMeshProUGUI totalStatsTimeLeft;
    public TextMeshProUGUI totalStatsDeaths;
    public TextMeshProUGUI totalStatsBlood;
    public TextMeshProUGUI totalStatsScore;

    public bool levelIsCompleted = false;

    void Start()
    {
        highscoreManager = GetComponent<Highscores>();
    }
    public void FinishLevel()
    {
        //gameManager.StartCoroutine("WaitForMeltAnimation");

        //levelIsCompleted = true;
        scoreManager.deathCount = PlayerPrefs.GetFloat("DeathCount");
        scoreManager.levelBloodLoss = bloodManager.bloodLevel;
        Debug.Log("DeathCount: " + scoreManager.deathCount);
        scoreManager.UpdateScore();

        Debug.Log("Level completed.");
        gameIsPaused = true;
        Debug.Log(">>>Setting timeScale to 0<<<");
        Time.timeScale = 0f;

        /*if (activeLevel.name=="Level 10"){
         * Congratulations! You beat the game!
         * something UI SetActive
         * }*/
        UICanvas.SetActive(false);

        levelCompleteWrapper.SetActive(true);
        Debug.Log("levelCompleteWrapper passed!");

        statsTitle.text = "Statistics " + gameManager.activeScene.name.ToString();
        statsTimeLeft.text = "Time left: " + gameManager.timeLeft.ToString("F2");
        statsDeaths.text = "Deaths: " + scoreManager.deathCount.ToString();
        statsBlood.text = "Blood left: " + bloodManager.bloodLevel.ToString();
        statsScore.text = "Score: " + scoreManager.levelScore.ToString("F2");

        if (gameManager.activeScene.name=="Level 10")
        {
            totalStatsTitle.text = "Total Statistics";
            totalStatsTimeLeft.text = "Time left: " + PlayerPrefs.GetFloat("TotalTime").ToString("F2");
            totalStatsDeaths.text = "Deaths: " + PlayerPrefs.GetFloat("TotalDeaths").ToString();
            totalStatsBlood.text = "Blood left: " + PlayerPrefs.GetFloat("TotalBloodLoss").ToString();
            totalStatsScore.text = "Score: " + PlayerPrefs.GetFloat("TotalScore").ToString("F2");
        }
      
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Nextlevel()
    {
        Debug.Log("Load next scene.");
        gameIsPaused = false;
        levelCompleteWrapper.SetActive(false);

        Destroy(FindObjectOfType<ImmortalHelperScript>().gameObject);

        levelIsCompleted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}

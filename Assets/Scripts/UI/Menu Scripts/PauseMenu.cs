﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public bool gameIsPaused = false;

    public GameManager gameManager;
    public BloodManager bloodManager;
    public LevelComplete levelComplete;
    public MainMenu mainMenu;

    public GameObject pauseMenuCanvas;
    public GameObject resumeButton;
    public GameObject retryButton;

    public TextMeshProUGUI levelTitleText;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && bloodManager.alive && !levelComplete.levelIsCompleted)
        {
            levelTitleText.text = gameManager.activeScene.name.ToString();
            retryButton.SetActive(false);
            resumeButton.SetActive(true);
            PausedGameCheck();
        }
	}

    public void PausedGameCheck()
    {
        

        if (gameIsPaused)
        {
            if (bloodManager.alive)
            {
                Resume();
                return;
            }
            else if (!bloodManager.alive)
            {
                Retry();
                return;
            }
        }
        Pause();
    }

    public void Resume()
    {
        Debug.Log("Game is unpaused.");
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }

    void Pause()
    {
        Debug.Log("Game is paused.");
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
    }

    public void LoadMenu()
    {
        if (levelComplete.level10Finished)
        {
            PlayerPrefs.SetFloat("TotalTime", 0);
            PlayerPrefs.SetFloat("TotalDeathCount", 0);
            PlayerPrefs.SetFloat("TotalBloodLoss", 0);
            PlayerPrefs.SetFloat("TotalScore", 0);
            PlayerPrefs.SetString("LastLevelPlayed", "Level 1");
            levelComplete.level10Finished = false;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        Debug.Log("Reload scene.");
        Debug.Log("DeathCount: "+ PlayerPrefs.GetFloat("DeathCount"));
        gameIsPaused = false;
        pauseMenuCanvas.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetFloat("LevelScore", 0);
        PlayerPrefs.SetFloat("LevelBloodLoss", 0);
        Time.timeScale = 1f;
    }
}

using System.Collections;
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
    public string sceneName;
    public bool levelIsCompleted = false;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
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
        if (sceneName == "Level 10")
        {
            Debug.Log("HEEEEEEJ " + Mathf.Round(PlayerPrefs.GetFloat("TotalScore")));
            Highscores.AddNewHighScore("Fis", Mathf.Round(PlayerPrefs.GetFloat("TotalScore")));
        }
        else
        {
            levelCompleteWrapper.SetActive(true);

            statsTitle.text = "Statistics " + gameManager.activeScene.name.ToString();
            statsTimeLeft.text = "Time left: " + gameManager.timeLeft.ToString("F2");
            statsDeaths.text = "Deaths: " + scoreManager.deathCount.ToString();
            statsBlood.text = "Blood left: " + bloodManager.bloodLevel.ToString();
            statsScore.text = "Score: " + scoreManager.levelScore.ToString("F2");
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public string [] levels;
    public Scene activeScene;

    public Slider bloodSlider;

    public BloodManager bloodManager;
    public PlayerController playerController;
    public ScoreManager scoreManager;
    public LevelComplete levelComplete;
    public PauseMenu pauseMenu;

    public GameObject pauseWrapper;
    public GameObject levelCompleteCanvas;
    public GameObject UICanvas;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bloodText;
    public TextMeshProUGUI deathsText;

    public float finishTime;
    public float timeLeft = 120.0f;
    public float reloadTime = 3f;

    public bool completedLevel = false;
    public bool melted = false;

    void Awake()
    {
        if (!FindObjectOfType<ImmortalHelperScript>())
        {
            GameObject immortalHelper = Resources.Load("ImmortalHelper") as GameObject;
            Instantiate(immortalHelper, new Vector3(0, 0, 0), Quaternion.identity);

            Debug.Log("Spawning the ImmortalHelper!");
        }
    }

    void Start ()
    {
        playerController = FindObjectOfType<PlayerController>();
        UICanvas.SetActive(true);
        pauseWrapper.SetActive(false);
        levelCompleteCanvas.SetActive(false);
        activeScene = SceneManager.GetActiveScene();
        if(activeScene.name == "Level 1" || activeScene.name == "Level 10")
            PlayerPrefs.SetString("LastLevelPlayed", activeScene.name);

        bloodManager.die += GameOverHolder;

        scoreManager.levelScore = 0;
        scoreManager.deathCount = PlayerPrefs.GetFloat("DeathCount");

        timerText.text = finishTime.ToString("F2");
        bloodText.text = bloodManager.bloodLevel.ToString();
        deathsText.text = scoreManager.deathCount.ToString();
    }
	
	void Update ()
    {
        if (timeLeft > 0 && playerController.started && (bloodManager.alive || !levelComplete.levelIsCompleted))
        {
            timeLeft -= Time.deltaTime;
            finishTime = timeLeft;
        }
        else if (timeLeft < 0 && playerController.started)
        {
            GameOverHolder();
        }
        else if (timeLeft > 0 && !bloodManager.alive)
        {
            GameOverHolder();
        }

        timerText.text = finishTime.ToString("F2");
        deathsText.text = PlayerPrefs.GetFloat("DeathCount").ToString();

        if (bloodManager.bloodLevel<=0)
        {
            bloodText.text = "0";
        }
        else
        {
            bloodText.text = bloodManager.bloodLevel.ToString();
        }

        bloodSlider.value = bloodManager.bloodLevel;
    }

    void GameOverHolder()
    {
        Debug.Log("GameOverHolder!");
        StartCoroutine("WaitForMeltAnimation");
    }

    IEnumerator WaitForMeltAnimation()
    {
        if (levelComplete.levelIsCompleted && !melted)
        {
            melted = true;
            if (SoundEffects.instance != null)
            {
                Debug.Log("Playing DeathWin!");
                SoundEffects.instance.DeathWin();
            }
            Debug.Log("Waiting for the animation...");
            playerController.body.constraints = RigidbodyConstraints2D.FreezeAll;
            playerController.anim.SetTrigger("Melt");
            yield return new WaitForSeconds(playerController.meltAnimation.length);
            Debug.Log("Done waiting for the animation!");
            levelComplete.FinishLevel();
        }
        else if (!levelComplete.levelIsCompleted && !melted)
        {
            melted = true;
            if (SoundEffects.instance != null)
            {
                Debug.Log("Playing DeathWin!");
                SoundEffects.instance.DeathWin();
            }
            Debug.Log("Waiting for the animation...");
            playerController.body.constraints = RigidbodyConstraints2D.FreezeAll;
            playerController.anim.SetTrigger("Melt");
            yield return new WaitForSeconds(playerController.meltAnimation.length);
            Debug.Log("Done waiting for the animation!");
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game over!");
        bloodManager.deathCounter += 1;

        pauseMenu.retryButton.SetActive(true);
        pauseMenu.resumeButton.SetActive(false);
        PlayerPrefs.SetFloat("DeathCount", (PlayerPrefs.GetFloat("DeathCount") + bloodManager.deathCounter));
        deathsText.text = PlayerPrefs.GetFloat("DeathCount").ToString();
        StartCoroutine(UIDelay());
    }

    IEnumerator UIDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(">>>Setting timeScale to 0<<<");
        Time.timeScale = 0f;
        pauseWrapper.SetActive(true);
    }
}
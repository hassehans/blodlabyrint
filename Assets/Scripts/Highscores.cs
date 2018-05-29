using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour
{
    static Highscores instance;
    public LevelComplete levelComplete;
    public MainMenu mainMenu;

    public Highscore[] highscoresList;
    DisplayHighscores highscoreDisplay;
    OnScoreAdded onScoreAdded;
    OnNameExists onNameExists;

    const string privateCode = "GliznKcRNEuWPkiQ4bsSjQTRwvLNZYWUSUzEQq5uyZHw";
    const string publicCode = "5b06a6e6191a850bcc28e065";
    const string webURL = "http://dreamlo.com/lb/";

    public string playerName;
    public float playerScore;

    delegate void OnScoreAdded();
    delegate void OnNameExists();

    public TextMeshProUGUI enterNameText;
    public TextMeshProUGUI enterNameFieldText;
    public TMP_InputField inputField;

    void Awake()
    {
        instance = this;
        highscoreDisplay = GetComponent<DisplayHighscores>();
    }

    public static void AddNewHighScore(string username, float score)
    {
        PlayerPrefs.SetString("LastLevelPlayed", "Level 1");
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, float score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload Successful");
        }
        else
        {
            Debug.Log("Error upploading: " + www.error);
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            highscoreDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            Debug.Log("Error Downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            float score = float.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            Debug.Log(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }

    IEnumerator Check(string playerName, float playerScore)
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {

            if (ContainsName(www.text, playerName))
            {
                enterNameText.text="Please enter another name.";
                enterNameFieldText.text="Try again!";
                //onNameExists();
            }
            else
            {
                AddNewHighScore(playerName, playerScore);
                //onScoreAdded();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void OnAddNamePressed()
    {
        playerName = inputField.text;
        playerScore = Mathf.Round(PlayerPrefs.GetFloat("TotalScore"));
        TryInsertingName(playerName, playerScore);

        //mainMenu.ResetAllPlayerPrefs();
        PlayerPrefs.SetFloat("TotalTime", 0);
        PlayerPrefs.SetFloat("TotalDeathCount", 0);
        PlayerPrefs.SetFloat("TotalBloodLoss", 0);
        PlayerPrefs.SetFloat("TotalScore", 0);
        PlayerPrefs.SetString("LastLevelPlayed", "Level 1");
    }

    public void TryInsertingName(string playerName, float playerScore)
    {  
        StartCoroutine(Check(playerName, playerScore));
        Time.timeScale = 1f;
    }

    private bool ContainsName(string textStream,string playerName)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            if (username == playerName)
            {
                print("name already exists");
                Debug.Log(username);
                return true;
            }
        }
        return false;
    }
}

public struct Highscore
{
    public string username;
    public float score;

    public Highscore(string _username, float _score)
    {
        username = _username;
        score = _score;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{

    const string privateCode = "GliznKcRNEuWPkiQ4bsSjQTRwvLNZYWUSUzEQq5uyZHw";
    const string publicCode = "5b06a6e6191a850bcc28e065";
    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;
    DisplayHighscores highscoreDisplay;
    static Highscores instance;
    public LevelComplete levelComplete;
    delegate void OnScoreAdded();
    OnScoreAdded onScoreAdded;
    delegate void OnNameExists();
    OnNameExists onNameExists;

    public TextMeshProUGUI enterNameText;
    public TextMeshProUGUI enterNameFieldText;

    void Awake()
    {
        instance = this;
        highscoreDisplay = GetComponent<DisplayHighscores>();

        TryInsertingName("Harald3", 1338);
    }

    public static void AddNewHighScore(string username, float score)
    {
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
                //enterNameText.text="Please enter another name.";
                //enterNameFieldText.text="Try again!";
                //onNameExists();
            }
            else
            {
                AddNewHighScore(playerName, playerScore);
                //onScoreAdded();
            }

        }
    }

    public void OnAddNamePressed()
    {

        //TryInsertingName();

    }
    

    public void TryInsertingName(string playerName, float playerScore)
    {
        
        StartCoroutine(Check(playerName, playerScore));
        
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
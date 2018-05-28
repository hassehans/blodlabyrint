using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //Load the first level in the game.
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.LogWarning("Change scene in NewGame method to correct scene please!");
    }

    //Quits the game.
    public void QuitGame()
    {
        Debug.Log("You quit the game!");
        Application.Quit();
    }

    //Quit to the menu.
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Highscore()
    {
        Debug.Log("View Highscore.");
        SceneManager.LoadScene("Highscore");
    }

    public void Credits()
    {
        Debug.Log("View Credits.");
        SceneManager.LoadScene("Credits");
    }

    public void Continue()
    {
        Debug.Log("Load last non-completed scene.");
        SceneManager.LoadScene(PlayerPrefs.GetString("LastLevelPlayed"));
    }
}

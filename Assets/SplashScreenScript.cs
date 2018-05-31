using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(32f);
        SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}

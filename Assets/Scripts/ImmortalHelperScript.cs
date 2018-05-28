using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalHelperScript : MonoBehaviour {

    bool existing = false;

    private void Awake()
    {
        if (!existing)
        {
            DontDestroyOnLoad(this.gameObject);
            existing = true;
            PlayerPrefs.SetFloat("DeathCount", 0);
        }
        else
        {
            Debug.Log("I'm alive suckers! - ImmortalHelper");
        }
    }

}

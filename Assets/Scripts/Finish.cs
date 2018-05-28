using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public LevelComplete levelComplete;
    public PlayerController playerController;
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name=="Player" && !playerController.attackActive)
        {
            levelComplete.levelIsCompleted = true;
            gameManager.StartCoroutine("WaitForMeltAnimation");

            Debug.Log("Finished the level!");
        }
    }
}

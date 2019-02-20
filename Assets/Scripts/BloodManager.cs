using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour {

    public float bloodLoss;
    public float bloodLevel;
    public float bloodStart;

    public bool alive;
    public int deathCounter;

    public GameObject player;
    public PlayerController movement;

    public Action die;
    
    void Start()
    {
        movement = player.GetComponent<PlayerController>();

        movement.looseBlood += BloodCalculation;

        bloodLoss = 1;
        bloodLevel = 50;
        bloodStart = bloodLevel;
        alive = true;
        deathCounter = 0;
    }

    void BloodCalculation()
    {
        if (bloodLevel > 0)
        {
            if (movement.attackActive)
                bloodLoss = 8;
            else if (!movement.attackActive)
                bloodLoss = 1;
            bloodLevel -= bloodLoss;
        }

        if (bloodLevel <= 0)
        {
            if (SoundEffects.instance != null)
                SoundEffects.instance.DeathWin();
            alive = false;

            die.Invoke();
        }
    }
}
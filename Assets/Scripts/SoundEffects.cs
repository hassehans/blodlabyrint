using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public static SoundEffects instance = null;

    public AudioSource wallCollision;
    public AudioSource splash;
    public AudioSource walkSound;
    public AudioSource deathWin;
    public AudioSource menuClick;
    public AudioSource menuHover;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }

    public void CollisionSound()
    {
        if (!wallCollision.isPlaying)
        {
            wallCollision.Play(); 
        }
        else if (wallCollision.isPlaying)
        {
            wallCollision.Play();
        }
    }
    public void Attacksound()
    {
        if (!splash.isPlaying)
        {
            splash.Play();
        }
    }

    public void WalkSound()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }
    public void DeathWin()
    {
        if (!deathWin.isPlaying)
        {
            deathWin.Play();
        }
    }

    public void MenuClick()
    {
        menuClick.Play();
    }

    public void MenuHover()
    {
        menuHover.Play();
    }
}
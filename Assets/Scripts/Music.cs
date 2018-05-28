using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;
    public AudioClip[] gameMusic;
    public AudioClip[] introMusic;
    public int currentSong = 0;

    private static Music instance;

    void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (music.isPlaying == false)
        {
            currentSong = (currentSong + 1) % gameMusic.Length;
            music.clip = gameMusic[currentSong];
            music.Play();
        }
    }
}
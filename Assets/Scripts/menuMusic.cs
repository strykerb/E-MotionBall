using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour
{
    public static bool AudioBegin = false;
    static bool stop = false;
    public static menuMusic musicinstance;

    void Awake()
    {
        
        if (musicinstance == null)
        {
            musicinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Object.Destroy(gameObject);
        }
        if (!AudioBegin)
        {
            playMusic();
        }
        /*
        if (!AudioBegin)
        {
            musicinstance.GetComponent<AudioSource>().Play();
            AudioBegin = true;
            Debug.Log("Audio Begin was false, now playing music and AudioBegin is now true");
        }
        else
        {
            Debug.Log("Audio Begin was true, not playing music");
        }
        */
    }

    public static void playMusic()
    {
        stop = false;
        musicinstance.GetComponent<AudioSource>().Play();
        Debug.Log("play music called");
    }

    public static void stopMusic()
    {
        stop = true;
        AudioBegin = false;
        Debug.Log("stop music called");
    }

    void Update()
    {
        if (stop)
        {
            musicinstance.GetComponent<AudioSource>().Stop();
        } 
    }
}




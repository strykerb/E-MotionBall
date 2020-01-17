using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour
{
    static bool AudioBegin = false;
    static bool stop = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }

    static void stopMusic()
    {
        stop = true;
    }

    void Update()
    {
        if (stop)
        {
            GetComponent<AudioSource>().Stop();
            AudioBegin = false;
        }
    }
}




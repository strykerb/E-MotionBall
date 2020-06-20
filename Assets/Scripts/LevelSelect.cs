using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    
    public void changeLevel(string sceneName)
    {
        if (menuMusic.musicinstance != null && !menuMusic.AudioBegin)
        {
            menuMusic.playMusic();
        }
        SceneManager.LoadScene(sceneName);
    }
}

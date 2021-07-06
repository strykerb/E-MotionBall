using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelChanger : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelSelect"))
        {
            GetComponentInChildren<Scrollbar>().value = 1.0f;
        }
    }

    public void changeLevel(Button b)
    {
        SceneManager.LoadScene(b.name);
    }

    public void nextLevel()
    {
        int nextLevelNumber = SceneManager.GetActiveScene().buildIndex;
        string nextLevelName = "Level" + nextLevelNumber;
        SceneManager.LoadScene(nextLevelName);
    }

    public void replayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

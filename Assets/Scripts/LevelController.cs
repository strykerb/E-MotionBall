using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    AndroidJavaObject intent;
    AndroidJavaObject currentActivity;
    String text;

    public void changeLevel(string sceneName)
    {
        if (sceneName == null)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        if (currentActivity != null)
        {
            intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            text = intent.Call<String>("getStringExtra", "level");
        } else
        {
            text = "level1";
        }
        changeLevel(text);
    }
}

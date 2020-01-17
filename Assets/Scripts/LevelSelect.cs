using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void changeLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackController : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name.Equals("LevelSelect"))
                {
                    Application.Quit();
                }
                else
                {
                    PlayerController pc = new PlayerController();
                    pc.exitGame();
                }
            }
        }
    }
}

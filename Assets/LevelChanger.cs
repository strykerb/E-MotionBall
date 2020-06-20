using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void changeLevel(Button b)
    {
        SceneManager.LoadScene(b.name);
    }
}

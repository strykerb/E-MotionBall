using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public Image[] Level;
    
    // Start is called before the first frame update
    void Start()
    {
        Level = GetComponentsInChildren<Image>();
        for (int i = 1; i < Level.Length-1; i++)
        {
            string lvlnum = Level[i].name;
            float score = PlayerPrefs.GetFloat(lvlnum, 0f);
            Text[] words = Level[i].GetComponentsInChildren<Text>();
           
            for (int j = 0; j < 2; j++)
            {
                if (words[j].name.Equals("HighScore")){
                    if (PlayerPrefs.GetFloat(lvlnum) != 0)
                    {
                        words[j].text = "Best Time: " + System.Math.Round(PlayerPrefs.GetFloat(lvlnum), 2);
                    }
                    else
                    {
                        words[j].text = "Best Time: - -";
                    }
                }
            }
            if (score == 0f)
            {
                Level[i+1].sprite = Resources.Load<Sprite>("Thumbnails/Locked");
                Level[i+1].GetComponentInParent<Button>().interactable = false;
            }
        }

        // populate last level high score
        string lastlvl = Level[Level.Length - 1].name;
        float lastscore = PlayerPrefs.GetFloat(lastlvl, 0f);
        Text[] lastwords = Level[Level.Length-1].GetComponentsInChildren<Text>();
        for (int j = 0; j < 2; j++)
        {
            if (lastwords[j].name.Equals("HighScore"))
            {
                if (PlayerPrefs.GetFloat(lastlvl) != 0)
                {
                    lastwords[j].text = "Best Time: " + System.Math.Round(PlayerPrefs.GetFloat(lastlvl), 2);
                }
                else
                {
                    lastwords[j].text = "Best Time: - -";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

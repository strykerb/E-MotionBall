using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHint : MonoBehaviour
{
    public Text introText;
    private bool created;
    private int count;
    private bool queued;

    private void Start()
    {
        count = 0;
        created = false;
        queued = false;
        introText = GameObject.Find("Intro Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (queued)
        {
            if (introText.text == "")
            {
                created = true;
                queued = false;
                introText.text = "ENTER THE PORTAL TO COMPLETE THE STAGE";
            }
        }
        if (created && count < 151)
        {
            count++;
        }

        if (count > 150)
        {
            removeIntro();
        }
    }

    public void removeIntro()
    {
        introText.text = "";
        created = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (introText.text != "")
            {
                queued = true;
            }
            else
            {
                introText.text = "ENTER THE PORTAL TO COMPLETE THE STAGE";
                created = true;
            }
        }
    }
}

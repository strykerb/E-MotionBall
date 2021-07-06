using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public GameObject Player;
    public AudioClip deathNoise;
    Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(-5.0f, 3.0f);
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerController.IncrementAttempts();
            PlayerController.Revert();
            RespawnPlayer();
        }


    }

    void RespawnPlayer() 
    {
        Player.SetActive(false);
        Player.transform.position = spawnPoint;
        Player.SetActive(true);
    }

}

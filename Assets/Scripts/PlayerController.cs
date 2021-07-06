using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerController : MonoBehaviour {

    public float speed = 8.0f;
    public static int numberOfAttempts;
    public static bool inverted;
    bool paused;
    public Text winText;
    public Text Attempts;
    public Text introText;
    public Image backDrop;
    public GameObject Player;
    public Rigidbody2D rb;
    public float moveHorizontal = 0.0f;
    public float moveVertical = 0.0f;
    public Button nextLevel;
    public Button retry;
    public Button home;
    public Button pause;
    public Button settings;
    public Button PrivPolicy;
    public float finishTime;
    public bool introExists;
    public int level;
    public AudioClip winNoise;
    public AudioClip deathNoise;
    Slider VolumeBar;
    
    AudioSource output;
    private float time;
    private static int InvertFactor;
    private bool gyroEnabled;
    private static bool win;
    private Gyroscope gyro;
    private AndroidJavaObject myClass;
    private AndroidJavaObject customClass;
    private AndroidJavaObject endClass;

    void Start ()
    {
        //gyroEnabled = EnableGyro();
        Canvas c = FindObjectOfType<Canvas>();
        if (c == null)
        {
            Debug.Log("Canvas is null");
        }
        Button[] buttons = c.GetComponentsInChildren<Button>(true);
        foreach (Button b in buttons)
        {
            if (b.CompareTag("PauseButton")){
                pause = b;
            }
            else if (b.name.Equals("NextButton"))
            {
                nextLevel = b;
            }
            else if (b.name.Equals("HomeButton"))
            {
                home = b;
            }
            else if (b.name.Equals("RetryButton"))
            {
                retry = b;
            }
            else if (b.name.Equals("PrivacyPolicy"))
            {
                PrivPolicy = b;
            }
        }
        nextLevel.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        PrivPolicy.gameObject.SetActive(false);

        VolumeBar = c.GetComponentInChildren<Slider>(true);
        winText = GameObject.Find("WinText").GetComponent<Text>();
        Attempts = GameObject.Find("AttemptText").GetComponent<Text>();
        introText = GameObject.Find("Intro Text").GetComponent<Text>();
        Image[] im = c.GetComponentsInChildren<Image>(true);
        foreach (Image i in im)
        {
            if (i.name.Equals("BackDrop")) {
                backDrop = i;
                Debug.Log("BackDrop Exists");
            }
        }
        backDrop.gameObject.SetActive(false);
        //backDrop.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        InvertFactor = 1;
        inverted = false;
        winText.text = "";
        numberOfAttempts = 1;
        SetAttemptText();
        win = false;
        time = 0;
        output = gameObject.GetComponent<AudioSource>();
        /*
        if (!introExists)
        {
            backDrop.gameObject.SetActive(false);
        }
        */
        Debug.Log(backDrop);
        introText.text = IntroData.data[SceneManager.GetActiveScene().buildIndex-2];
        paused = false;
        resumeGame();
    }

    private void Awake()
    {
        if (menuMusic.musicinstance != null && !menuMusic.AudioBegin)
        {
            menuMusic.playMusic();
        }
    }

    public static void Revert()
    {
        inverted = false;
        InvertFactor = 1;
    }

    private void Update()
    {
        SetAttemptText();
        time += Time.deltaTime;
        if (introExists && time > 3)
        {
            introText.text = "";
            //backDrop.gameObject.SetActive(false);
            introExists = false;
        }
        if (win) 
        {
            StageComplete();
        }
    }

    void FixedUpdate ()
    {
        if (inverted)
        {
            InvertFactor = -1;
        }
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            moveHorizontal = InvertFactor * Input.GetAxis("Horizontal");
            moveVertical = InvertFactor * Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = InvertFactor * Input.acceleration.x;
            moveVertical = InvertFactor * Input.acceleration.y;
        }

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rb.AddForce (movement * speed);
    }

    public void StageComplete()
    {
        PlayAudio(1);
        winText.text = "Level Complete!" + System.Environment.NewLine + "in " + numberOfAttempts.ToString() + " Attempts";
        backDrop.gameObject.SetActive(true);
        finishTime = time;
        gameObject.SetActive(false);
        Revert();
        nextLevel.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        home.gameObject.SetActive(true);
        float prevHigh = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name, 999f);
        if (finishTime < prevHigh)
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, finishTime);
        }
    }

    public void togglePause()
    {
        removeIntro();
        if (backDrop == null)
        {
            PlayerController playerinstance = FindObjectOfType<PlayerController>();
            if (playerinstance != null)
            {
                playerinstance.togglePause();
            }
            return;
        }
        if (paused)
        {
            paused = false;
            backDrop.gameObject.SetActive(false);
            resumeGame();
        } 
        else
        {
            paused = true;
            backDrop.gameObject.SetActive(true);
            pauseGame();
        }
    }
    void resumeGame()
    {
        Time.timeScale = 1;
        //backDrop.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        PrivPolicy.gameObject.SetActive(false);
        VolumeBar.gameObject.SetActive(false);
        winText.text = "";
        Sprite[] IconsAtlas = Resources.LoadAll<Sprite>("Textures/theme-1-3-e");
        // Get specific sprite
        Sprite settingsSprite = IconsAtlas.Single(s => s.name == "settings");
        //Sprite s = Resources.Load<Sprite>("Textures/theme-1-3-e_10.png");
        pause.GetComponent<Image>().overrideSprite = settingsSprite;
    }

    void pauseGame()
    {
        
        Time.timeScale = 0;
        home.gameObject.SetActive(true);
        VolumeBar.gameObject.SetActive(true);
        PrivPolicy.gameObject.SetActive(true);
        winText.text = "Paused";
        Sprite[] IconsAtlas = Resources.LoadAll<Sprite>("Textures/theme-1-4-c");
        // Get specific sprite
        Sprite settingsSprite = IconsAtlas.Single(s => s.name == "close");
        //Sprite s = Resources.Load<Sprite>("Textures/theme-1-3-e_10.png");
        pause.GetComponent<Image>().overrideSprite = settingsSprite;
    }

    void SetAttemptText()
    {
        Attempts.text = "Time: " + System.Math.Round(time, 2);
    }

    public static void IncrementAttempts() 
    {
        numberOfAttempts++;
    }

    public static void Win() 
    {
        win = true;
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            inverted = false;
            PlayAudio(0);
            Debug.Log("Dead");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Invert"))
        {
            inverted = true;
        }
        if (other.gameObject.CompareTag("Spikes"))
        {
            inverted = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Invert"))
        {
            Revert();
        }
    }

    public void exitGame()
    {
        menuMusic.stopMusic();
        if (paused)
        {
            togglePause();
        }
        SceneManager.LoadScene("LevelSelect");
    }

    public void removeIntro()
    {
        if (introText != null) { introText.text = ""; }
    }

    public void PlayAudio (int idx)
    {
        if (idx == 0)
        {
            AudioSource.PlayClipAtPoint(deathNoise, new Vector3(0, 0, 0), PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        }
        else if (idx == 1)
        {
            AudioSource.PlayClipAtPoint(winNoise, new Vector3(0, 0, 0), PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        }
    }
}
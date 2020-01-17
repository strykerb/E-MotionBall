using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 8.0f;
    public static int numberOfAttempts;
    public static bool inverted;
    public Text winText;
    public Text Attempts;
    public Text introText;
    public  Image backDrop;
    public GameObject Player;
    public Rigidbody2D rb;
    public float moveHorizontal = 0.0f;
    public float moveVertical = 0.0f;
    public Button nextLevel;
    public Button retry;
    public Button home;
    public int finishTime;
    public bool introExists;
    public int level;
    public AudioClip winNoise;

    private int time;
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
        rb = GetComponent<Rigidbody2D>();
        nextLevel.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        rb.gravityScale = 0;
        InvertFactor = 1;
        winText.text = "";
        numberOfAttempts = 1;
        SetAttemptText();
        win = false;
        time = 0;
        if (!introExists)
        {
            backDrop.gameObject.SetActive(false);
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
        time++;
        if (introExists && time > 150)
        {
            introText.text = "";
            backDrop.gameObject.SetActive(false);
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
        AudioSource.PlayClipAtPoint(winNoise, transform.position);
        winText.text = "Level Complete!" + System.Environment.NewLine + "in " + numberOfAttempts.ToString() + " Attempts";
        backDrop.gameObject.SetActive(true);
        finishTime = time;
        Player.SetActive(false);
        Revert();
        nextLevel.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        home.gameObject.SetActive(true);
        startService("com.buffingtonstudios.broadcastjar.MyServiceStarter");
    }

    void startService(string packageName)
    {
        AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        customClass = new AndroidJavaObject(packageName);
        customClass.Call("StartService", unityActivity, numberOfAttempts, time, level);
        customClass.Dispose();
    }

    void SetAttemptText()
    {
        Attempts.text = "Attempt " + numberOfAttempts.ToString();
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
        AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        endClass = new AndroidJavaObject("com.buffingtonstudios.broadcastjar.EndReceiverBroadcaster");
        endClass.Call("EndService", unityActivity);
        unityActivity.Dispose();
        endClass.Dispose();
        Application.Quit();
    }

}
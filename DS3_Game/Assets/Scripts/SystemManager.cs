using UnityEngine;
using TMPro;

public class SystemManager : MonoBehaviour
{
    public GameObject button;
    public GameObject timer;
    public bool gameRunning;
    public static SystemManager instance;

    public float gameTime;
    private float remainingTime;
    public TMP_Text timerText;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        resetTimer();
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimer();
            }
            else
            {
                Debug.Log("Time is Over!");
                remainingTime = 0;
                button.SendMessage("ResetButton", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void UpdateTimer()
    {
        int seconds = (int)remainingTime % 60;
        timerText.text = seconds.ToString();

    }

    public void resetTimer()
    {
        remainingTime = gameTime;
    }

}

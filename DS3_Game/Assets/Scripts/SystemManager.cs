using UnityEngine;
using TMPro;

public class SystemManager : MonoBehaviour
{
    public GameObject button;
    public bool gameRunning;
    public static SystemManager instance;

    public GameObject timer;
    public float gameTime;
    private float remainingTime;
    public TMP_Text timerText;

    public int points;
    public GameObject pointPanel;
    public TMP_Text pointText;

    public GameObject spawner_red;
    public GameObject spawner_blue;
    public GameObject spawner_rabbit;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        resetGame();
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
                pointText.text = "Score: "+points.ToString();
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
    

    public void resetGame()
    {
        remainingTime = gameTime;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float remainingTime;
    private bool gameRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimer();
    }

    // Update is called once per frame
    //If gameRunning is true, the timer will be reduced every frame, depending on how much time passed
    //If timer reaches zero, will print message on console, and end game by changing gameRunning to False
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
                gameRunning = false;
                remainingTime = 0;
            }
        }
    }

    //updates the actual time text
    //have to convert to int, as if not the time change every millisecond, which is too much change on the screen
    void UpdateTimer()
    {
        int seconds = (int)remainingTime % 60;
        SystemManager.instance.timeText.text = seconds.ToString();

    }
}

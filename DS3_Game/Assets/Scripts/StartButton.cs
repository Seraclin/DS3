using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    public void StartTimer()
    { 
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        SystemManager.instance.gameRunning = true;
        SystemManager.instance.timer.SetActive(true);
    }

    public void ResetButton()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        SystemManager.instance.gameRunning = false;
        SystemManager.instance.timer.SetActive(false);
    }

}

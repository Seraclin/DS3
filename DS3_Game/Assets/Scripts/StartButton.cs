using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // for text

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    public GameObject timeManager;
    public void StartTimer()
    { 
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        SystemManager.instance.gameRunning = true;
        SystemManager.instance.timer.SetActive(true);
        SystemManager.instance.spawner_red.SetActive(true);
        SystemManager.instance.spawner_blue.SetActive(true);
        SystemManager.instance.points = 0;

        gameObject.GetComponent<AudioSource>().Play(); // play audio

        // remove start button from visibility, so player can shoot properly
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    public void ResetButton()
    {
        // make start button reappear
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Shoot to Start";
        gameObject.GetComponent<AudioSource>().Play(); // play audio

        SystemManager.instance.gameRunning = false;
        SystemManager.instance.timer.SetActive(false);
        SystemManager.instance.spawner_red.SetActive(false);
        SystemManager.instance.spawner_blue.SetActive(false);
        SystemManager.instance.resetGame();
    }

}

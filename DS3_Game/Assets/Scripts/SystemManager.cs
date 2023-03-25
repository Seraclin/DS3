using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemManager : MonoBehaviour
{
    public GameObject cube;
    
    // Start is called before the first frame update
    void Start()
    {
        cube.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TimeManager.instance.gameRunning = true;
            cube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            TimeManager.instance.timer.SetActive(true);
        }
    
    }
}

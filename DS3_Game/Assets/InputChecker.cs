using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used to detect what keys were pressed and print to the Console.
 * */
public class InputChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectPressedKeyOrButton();
    }

    public void detectPressedKeyOrButton()
    {
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("Keycode down: " + kcode);
            }
        }
    }
}

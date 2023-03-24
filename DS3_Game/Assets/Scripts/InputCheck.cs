using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputCheck : MonoBehaviour
{

    public TMP_Text keyCode;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0)){
            keyCode.text = "Shoot";
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    private void Awake()
    {
        instance = this;
    }

    public TMP_Text timeText;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

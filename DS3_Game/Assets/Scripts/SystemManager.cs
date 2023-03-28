using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public GameObject button;
    public GameObject timer;
    public bool gameRunning;
    public static SystemManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

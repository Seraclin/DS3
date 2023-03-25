using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to demonstrate basic 3D movement and collision
// Attach to gameObject with Rigidbody to move with controls
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player; // public vars shows up in the Inspector
    private int hiddenCounter = 2; // private vars won't show up in the Inspector
    [SerializeField] private int points = 0; // private, but will shows up in Inspector

    Rigidbody rb; // you need this for doing Vectors

    // Start is called before the first frame update
    void Start()
    {
        // A basic print statement in the Console
        Debug.Log("Hello World!");

        // get the Rigidbody component on this gameObject and assign to a variable
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // When U key is pressed, increase points by 1
        if (Input.GetKeyDown(KeyCode.U))
        {
            points += 1;
        }

        // (x, y, z) = positional 3D axis, x = left/right, y = up/down, z = forward/backwards; requires a Rigidbody
        // use Input.GetKey(key) for holding down key; Input.GetKeyDown(key) for one action per keystroke; 
        // Press up arrow to move object forward, while key is held down
        if (Input.GetKey(KeyCode.H))
        {
            rb.velocity = new Vector3(0, 0, 4f); // this is the (x, y, z) of the Rigidbody
        }

        // Press down arrow to move backwards, once per key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.velocity = new Vector3(0, 0, -4f);
        }

        // A better way is to use Input.getAxis(), works with WASD or arrow keys
        // https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontal*4f, rb.velocity.y, vertical*4f);
        */

    }

    // When two colliders first touch, both objects need a collider on them
    private void OnCollisionEnter(Collision collision)
    {
        // Print the object you collided with and change it to red
        Debug.Log("You touched:" + collision.collider.gameObject.name);
        if(collision.collider.gameObject.CompareTag("Bullet"))
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

}

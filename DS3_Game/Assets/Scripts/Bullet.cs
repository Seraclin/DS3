using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script should be on Assets/Modern Guns - Handgun/_Prefabs/45ACP Bullet_Head.prefab
 * Makes sure the bullets disappear when they collide with an object (or after a certain time)
 * Probably should label gameObjects with tags like "Wall" or "Enemy"
 * to define different collision behavior.
 *  
 */
public class Bullet : MonoBehaviour
{
    public float duration = 60f; // how long bullets should last before being destroyed (seconds)

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration); // Note: Destroy will terminate script here since this gameObject is deleted
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy bullet when colliding with a wall or any untagged game object
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Untagged")
        {
            // Put code here
            
            Destroy(gameObject, 0.1f); // Note: Destroy will terminate script here since this gameObject is deleted
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * A reticle for VR
 * Unlike a regular 2D reticle, must be rendered on Canvas - World Space and must have size/distance be adjusted
 * in order to not clip into things.
 * There's also some math involved to make it not blurry and more accurate
 */
public class Reticle : MonoBehaviour
{
    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        // Point gun towards reticle
        // gun.transform.LookAt(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

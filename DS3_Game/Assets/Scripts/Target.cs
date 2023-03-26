using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script for Targets
 * Currently, destroys itself after being hit by a "Bullet or gun Raycast from SimpleShoot.cs
 * You can inherit from this class e.g. public class TargetSpecial:Target
 * to define your own behavior
*/

public class Target : MonoBehaviour
{
    public int score = 10; // score to increment by
    public GameObject gameManager; // game manager for score
    public GameObject particleEffect; // particle effect when target is destroyed
    // Don't need start/update()

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("You hit the target");
            // TODO: add points to player's score or something
            if (gameManager)
            {
                // put code here
            }
            Destroy(gameObject);
        }
    }
    public void playTargetParticle(object[] location)
    {
        // [0] = position, [1] = rotation
        GameObject impactObj = Instantiate(particleEffect, (Vector3)location[0], (Quaternion)location[1]);
    }
    public void DestroyTarget()
    {   // public call makes it easier to deal with
        Debug.Log("Target has been hit");
        // TODO: add points to player's score or something
        if (gameManager)
        {
            // put code here
        }
        Destroy(gameObject);
    }
}

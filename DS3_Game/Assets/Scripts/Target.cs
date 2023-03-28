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
    { // only if using physical rigidbody bullet object
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
        if (collision.gameObject.CompareTag("Despawn"))
        { // just in case, but it should go to Trigger
            Debug.Log("Target despawned");
            // Clean up targets after out of view
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Despawn"))
        {
            Debug.Log("Target despawned (trigger)");
            // Clean up targets after out of view
            Destroy(gameObject);
        }
    }

    public void playTargetParticle(object[] location)
    {
        // [0] = position, [1] = rotation
        GameObject impactObj = Instantiate(particleEffect, (Vector3)location[0], (Quaternion)location[1]);
    }
    public void DestroyTargetPlayer()
    {   // When the target is destroyed specifically by a player's gun
        // public call makes it easier to deal with
        Debug.Log("Target has been hit");
        //add points to player's score
        SystemManager.instance.points += score;
        Destroy(gameObject);
    }
    public IEnumerator MoveObject(object[] positions)
    {
        // coroutine for moving, [0] = start position, [1] = end position, [2] spawn velocity (duration to travel across)
        float elapsedTime = 0f;
        Vector3 startPosition = (Vector3)positions[0];
        Vector3 endPosition = (Vector3)positions[1];
        float spawnVelocity = (float)positions[2];

        while (elapsedTime < spawnVelocity)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / spawnVelocity));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition; // Ensure the object is in the exact end position when the coroutine ends
    }

    public void Update()
    {
        if (!SystemManager.instance.gameRunning)
        {
            Destroy(gameObject);
        }
    }
}

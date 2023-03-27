using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script for spawning targets along a flat plane (kinda)
 * This should be on the TargetSpawner prefab which has a
 * LeftSpawn and RightSpawn side along with L/R despawn
 * For now, the target will be generated on LeftSpawn and travel to the right.
 * Then when it hits the Despawn collider (isTrigger) it should disappear.
 * Spawns targets every spawnInterval (sec) travelling at spawnVelocity
 * TODO: generate from either direction left to right or right to left
 */

public class TargetSpawner : MonoBehaviour
{
    public GameObject leftSpawn;  // child ref left spawn
    public GameObject rightSpawn; // child ref right spawn
    public GameObject leftDespawn;  // child ref left despawn
    public GameObject rightDespawn; // child ref right despawn
    public GameObject spawnObject; // what gameObject (target) to spawn at the spawner
    //public GameObject[] spawnObjects; // multiple gameObjects (targets) to spawn at the spawner
    public GameObject gameManager; // ref to gameManager script (just in case)

    // timing variables
    private float elapsedTime = 0.0f; // timer, probably should be based off gameManager time
    public float spawnInterval = 3.0f; // seconds to wait before generating a new target
    public float spawnVelocity = 20f; // how long it should take for spawned Target to travel across screen to despawn zone
    // lower spawnVelocity = faster

    // spawn area y-axis range
    private float spawnTopY;
    private float spawnBottomY;

    // Start is called before the first frame update
    void Start()
    {
        // First we need to find our spawning range vertically in world space
        BoxCollider boxColliderL = leftSpawn.GetComponent<BoxCollider>();
        Vector3 spawnWorldTop = gameObject.transform.TransformPoint(boxColliderL.center + new Vector3(0, boxColliderL.size.y / 2f, 0));
        Vector3 spawnWorldBottom = gameObject.transform.TransformPoint(boxColliderL.center - new Vector3(0, boxColliderL.size.y / 2f, 0));
        //Vector3 spawnLocalY = new Vector3(0f, leftSpawn.transform.localScale.y / 2f, 0f); // doesn't work
        //Vector3 spawnWorldTop = gameObject.transform.TransformPoint(leftSpawn.transform.position + spawnLocalY);
        //Vector3 spawnWorldBottom = gameObject.transform.TransformPoint(leftSpawn.transform.position - spawnLocalY);
        spawnTopY = spawnWorldTop.y;
        spawnBottomY = spawnWorldBottom.y;

        // Next, for x-axis we will just spawn in the middle of the spawner
        // For z-axis we keep constant
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnObject) // TODO: add to this if statement, to make it not start automatically (refer to gameManager)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > spawnInterval)
            {
                targetSpawn();
            }
        }
    }
    public void targetSpawn()
    {
        elapsedTime = 0; // reset timer
        // Note: I'm doing this all in World Space
        // Randomly choose y (inclusive) to generate target while keeping x and z constant
        float randomY = Random.Range(spawnBottomY, spawnTopY); // random y in world space
        float worldXL = leftSpawn.transform.TransformPoint(Vector3.zero).x; // left spawner x value in world space
        float worldXR = rightSpawn.transform.TransformPoint(Vector3.zero).x; // right spawner x value in world space

        // spawn object and go from left to the right
        Vector3 spawnPosition = new Vector3(worldXL, randomY, gameObject.transform.position.z);
        Vector3 despawnPosition = new Vector3(rightDespawn.transform.position.x, randomY, gameObject.transform.position.z);
        GameObject objectToMove = Instantiate(spawnObject, spawnPosition, Quaternion.identity);
        float startTime = Time.time; // set startTime to current time

        // Lerp: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        // some time calculations to ensure that it move smoothly regardless of framerate, coroutine?
        //float distanceCovered = (Time.time - startTime) * spawnVelocity;
        //float journeyLength = Vector3.Distance(spawnPosition, despawnPosition);
        //float fractionOfJourney = distanceCovered / journeyLength;
        //objectToMove.transform.position = Vector3.Lerp(spawnPosition, despawnPosition, fractionOfJourney);

        // Get a reference to the object that has the moveObject script with the coroutine
        Target script = objectToMove.GetComponent<Target>();
        object [] movementParams = { spawnPosition, despawnPosition, spawnVelocity };
        script.StartCoroutine(script.MoveObject(movementParams));
    }
}

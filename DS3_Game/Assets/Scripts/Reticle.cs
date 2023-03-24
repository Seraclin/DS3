using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * A reticle for VR, attach to reticle UI object
 * Unlike a regular 2D reticle, must be rendered on Canvas - World Space and must have size/distance be adjusted
 * in order to not clip into things or appear blurry when close to an object.
 * This uses raycasting to make sure reticle appears nicely
 */
public class Reticle : MonoBehaviour
{
    public GameObject gun;

    private float defaultPosZ;

    void Start()
    {
        defaultPosZ = transform.localPosition.z;
    }

    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.rotation * Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= defaultPosZ)
            {
                transform.localPosition = new Vector3(0, 0, hit.distance-0.2f); // little z distance correction to not clip
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, defaultPosZ);
            }
        }
    }
}

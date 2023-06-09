﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    public GameObject line; // LineRenderer for raycast
    public Camera fpsCam; // shoot towards camera rather than from gun barrel
    public GameObject impactEffect; // gun impact particle
    private AudioSource gunNoise; // gun shot sound component

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    // TODO: sounds, raycast, reloading

    // Sound effects for gun (e.g. shooting, reloading, empty, casing eject)

    // Bullet ray cast (i.e. bullet trail)

    // Limited ammo and reload mechanic 


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        gunNoise = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // If you want a different input, change it here
        // Docs: https://docs.unity3d.com/ScriptReference/Input.GetButtonDown.html
        // Edit > Project Settings > Input Manager to bring up the Input Manager
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.H))
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");
        }
    }


    //This function creates the bullet behavior
    public void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // TODO: muzzle sound effect

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }
        if (gunNoise)
        {   // Play gun shot sound
            gunNoise.Play();
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }
       
        // ==== Calculate raycast from player's camera ====
        RaycastHit hitInfo;
        // bool hasHit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitInfo, 100); // position, forward (direction), out --> hitInfo, range; Note: you can use barrelLocation to shoot from gun (inaccurate)
        bool hasHit = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, 500); // shoots towards camera
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green, 1f, false); // debug
        //Debug.Log(hitInfo.transform.name); // debug

        // Create a bullet and add force on it in direction of the barrel
        // Note: this also applies a little recoil to the gun, so might want to disable Rigidbody or isKinematic=True
        // use Rigidbody.velocity for constant speed and AddForce for add a force
        //Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
        // Add a visible line and bullet from gun barrel to where we hit with raycast (not recommended)
        if (line)
        {
            GameObject liner = Instantiate(line);
            // Set positionCount first to your new array size and THEN call the SetPositions with your new array
            liner.GetComponent<LineRenderer>().positionCount = 2;
            if (hasHit)
            {
                // line
                liner.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position, hitInfo.point});
                Debug.DrawRay(barrelLocation.position, hitInfo.point, Color.blue, 1f, false); // debug

                // bullet (not recommended)
                //Vector3 bulletDirection = hitInfo.point - barrelLocation.position;
                //Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().velocity = bulletDirection * shotPower; ;
            }
            else
            {
                // line
                liner.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position, barrelLocation.position + barrelLocation.forward * 100});
                Debug.DrawRay(barrelLocation.transform.position, barrelLocation.transform.forward * 100, Color.green, 1f, false); // debug

                // bullet (not recommended)
                //Vector3 bulletDirection = (fpsCam.transform.position + fpsCam.transform.forward * 1000) - barrelLocation.position;
                //Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().velocity = bulletDirection * shotPower;
            }

            Destroy(liner, 0.2f); // destroy line after 0.5 secs
        }

        

        // Destroy target hit by Raycast via Target's DestroyTarget() method
        // SendMessage docs: https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html
        if (hasHit && hitInfo.collider.gameObject.CompareTag("Target"))
        {
            // add target specific impact particle effect
            //  Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            //GameObject impactObj = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), hitInfo.transform); // particle as child
            object[] parameters = { hitInfo.point, Quaternion.LookRotation(hitInfo.normal) };
            hitInfo.collider.gameObject.SendMessage("playTargetParticle", parameters, SendMessageOptions.DontRequireReceiver);
            hitInfo.collider.gameObject.SendMessage("DestroyTargetPlayer", SendMessageOptions.DontRequireReceiver);
        }
        else if (hasHit && hitInfo.collider.gameObject.CompareTag("Button"))
        {
            // same as hitting target, but specifically for button
            if (!SystemManager.instance.gameRunning)
            {
                if (impactEffect)
                {
                    Debug.Log("button hit");
                    //Once the button is hit, the button will execute StartTimer function.
                    GameObject impactObj = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    hitInfo.collider.gameObject.SendMessage("StartTimer", SendMessageOptions.DontRequireReceiver);
                    Destroy(impactObj, 2f);
                }
            }
        }
        else if (hasHit)
        {
            // add a little hit impact particle effect to where raycast hits (not target specific)
            if (impactEffect)
            {
                //  Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
                //GameObject impactObj = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), hitInfo.transform); // particle as child
                GameObject impactObj = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactObj, 2f);
            }
        }

        // Optional: Add a little impact force to object's Rigidbody that we hit
        //if(hitInfo.rigidbody != null)
        //{
        //  hitInfo.rigidbody.AddForce(-hitInfo.normal * 20f);
        //}

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // TODO: casing sound effect 

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}

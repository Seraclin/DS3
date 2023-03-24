using System.Collections;
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

    }

    void Update()
    {
        // If you want a different input, change it here
        // Docs: https://docs.unity3d.com/ScriptReference/Input.GetButtonDown.html
        // Edit > Project Settings > Input Manager to bring up the Input Manager
        if (Input.GetButtonDown("Fire1"))
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

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        // TODO: add script to bullet prefab to do something on collision (like destroy itself)
        // Note: this also applies a little recoil to the gun, so might want to disable Rigidbody or isKinematic=True
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
        // Add raycast (i.e. laser beam line) to gun
        RaycastHit hitInfo;
        // bool hasHit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitInfo, 100); // position, forward (direction), out --> hitInfo, range; Note: you can use barrelLocation to shoot from gun (inaccurate)
        bool hasHit = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, 100); // shoots from camera
        if (line)
        {
            GameObject liner = Instantiate(line);
            // Set positionCount first to your new array size and THEN call the SetPositions with your new array
            liner.GetComponent<LineRenderer>().positionCount = 2;
            if (hasHit)
            {
                liner.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position, hitInfo.point});
                Debug.DrawRay(barrelLocation.position, barrelLocation.forward, Color.blue, 1f, false); // debug
            }
            else
            {
                liner.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position, barrelLocation.position + barrelLocation.forward * 100});
                Debug.DrawRay(barrelLocation.transform.position, barrelLocation.transform.forward * 100, Color.green, 1f, false); // debug
                // Debug.Log("Not Hit");

            }

            Destroy(liner, 0.2f); // destroy line after 0.5 secs
        }

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

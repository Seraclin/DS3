using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Gyroscope = UnityEngine.InputSystem.Gyroscope; // so you can just write out Gyroscope

/*
 * Detect what keys were pressed or read input sensor data, and print to the Console for debugging.
 * Input System (sensors): https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Sensors.html
 * This is useful for debugging with Unity Remote (with USB and phone) without needing to build/export the project.
 * Adjust inputs in "XRI Default Input Actions" asset menu
 * */
public class InputChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // apparently can't put sensor enabling in start because it happens after game start
        // (by default sensors are not enabled)
    }

    // Update is called once per frame
    void Update()
    {
        detectPressedKeyOrButton();

        // Enable gyroscope sensor for headtracking = XRI Head > position, these are Vector3 (angular velocity)
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.LogWarning("System does not support Gyroscope");
            return;
        }

        if (Gyroscope.current != null)
        {
            if (Gyroscope.current.enabled)
            {
                Debug.Log("InputSystem Gyro: "+Gyroscope.current.angularVelocity.ReadValue()); // Vector3
            }
            else
            { // in case for some reason gyro gets randomly disabled, reenable it
                Debug.Log("Gyroscope not enabled. Enabling now.");
                InputSystem.EnableDevice(Gyroscope.current);
            }
        }

        // Enable attitude sensor for headtracking = XRI Head > rotation, these are Quatenions
        if (AttitudeSensor.current != null)
        {
            if (AttitudeSensor.current.enabled)
            {
                Debug.Log("InputSystem Attitude: " + AttitudeSensor.current.attitude.ReadValue()); // Quaternion
            }
            else
            { // in case for some reason attitude gets randomly disabled, reenable it
                Debug.Log("AttitudeSensor not enabled. Enabling now.");
                InputSystem.EnableDevice(AttitudeSensor.current);
            }
        }

        // some math for head rotations:
        // Quaternion rot = Quaternion.Inverse(new Quaternion(orientation.x, orientation.y, -orientation.z, orientation.w));
        // Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)) * rot;

        // Note: using gyro/attitude sensor disables XR Device Simulator position/rotation tracking
        // In order to reenable, rebind the XRI Default Input Actions>centerEyePosition/Rotation [XR HMD]
    }

    public void detectPressedKeyOrButton()
    {
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("Keycode down: " + kcode);
            }
        }
    }
}

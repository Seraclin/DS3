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
 * Make sure to disable any TrackedPoseDriver(s) when using this, and vice versa
 * Note: make sure XR Device Simulator is also in the scene, also this rotates with respect to world rotation so this can't be a child of another object
 * */
public class GyroController : MonoBehaviour
{
    // Phone is sideways so need to correct rotation for Camera
    private Quaternion correctionQuaternion;
    public Camera playerCam;  // assign player cam via inspector
    public GameObject parentCam; // parent of camera (e.g. CameraOffset) for calibration

    private Quaternion offset; // offset of camera to start where user is currently facing rather than device default
    // Start is called before the first frame update
    void Start()
    {
        // apparently can't only put sensor enabling in start because it happens after game start
        // (by default sensors are not enabled)

        if (Gyroscope.current != null)
        {
            if (Gyroscope.current.enabled)
            {
                Debug.Log("Start InputSystem Gyro: " + Gyroscope.current.angularVelocity.ReadValue()); // Vector3
            }
        }

        if (AttitudeSensor.current != null)
        {
            if (AttitudeSensor.current.enabled)
            {
                Debug.Log("Start InputSystem Attitude: " + AttitudeSensor.current.attitude.ReadValue()); // Quaternion
            }

        }

        // get the initial gyroscope reading
        correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
        offset = parentCam.transform.rotation; // subtraction of Quaternion is by multiplying by Quternion.Inverse
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
                // Debug.Log("InputSystem Gyro: "+Gyroscope.current.angularVelocity.ReadValue()); // Vector3
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
                // Debug.Log("InputSystem Attitude: " + AttitudeSensor.current.attitude.ReadValue()); // Quaternion
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
        // Also gyroscope readings are lefthanded while Unity's are righthanded
        // Warning: TrackedPoseDriver and this won't work while both are active as the former overrides the rotations
        if(playerCam != null)
        {
            GyroModifyCamera();
        }
        else
        {
            Debug.LogWarning("No Camera on Player!");
        }

        // J key - Reset gyro position towards current phone orientation
        if (Input.GetKeyDown(KeyCode.J))
        {
            offset = correctionQuaternion * GyroToUnity(Input.gyro.attitude);
        }
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

    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        Quaternion gyroQuaternion = GyroToUnity(Input.gyro.attitude);
        // rotate coordinate system 90 degrees. Correction Quaternion has to come first
        Quaternion calculatedRotation = correctionQuaternion * gyroQuaternion;
        // TODO: account for offset of device

        playerCam.transform.rotation = calculatedRotation;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        // Phone gyro (right handed) has to be converted to Unity Quaternion (left handed)
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}

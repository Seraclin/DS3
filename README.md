# DS3: Design For Another World
VR Game for DS3: Design For Another World for CS485-1 Human Computer Interactions Spring 2023 at Emory University 
<br>
Made in Unity 2021.3.20f1
<br>
By Samantha Lin [@Seraclin](https://github.com/Seraclin), Emma Klein, Muhammad Shahid, Allen Zhang [@theJokerEvoker
](https://github.com/theJokerEvoker), Jonathan Kim [@JKim0212](https://github.com/JKim0212)

# Tips and Tricks
I used Unity 2021.3.20f1 on Windows 10 with an iPhone for VR development. And here are some common things I came across. There might be different behavior for MacOS or Android.
## General Unity Setup
* If you have a supported VR headset (e.g. Oculus) and motion controllers, you should use the [XR Interactions Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.3/manual/general-setup.html) package. You can also use [XR Device Simulator](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.3/manual/xr-device-simulator-overview.html) to simulate a VR headset. Both packages can be installed via "Tools > Package Manager > XR Interaction Toolkit" and make sure to import the Samples: "Starter Assets" and optionally the "XR Device Simulator". You can find the controls for the device simulator by double-clicking the Default XRI Input Actions asset or opening the Input Manager. A good XR tutorial can be found [here](https://www.youtube.com/watch?v=5ZBkEYUyBWQ). 

## Git and Version Control
* If you are using Git for version control, make sure to use the default Unity `.gitignore`. You might have to remove the front '/' for the lines `[Ll]ibrary/` to `[Mm]emoryCaptures/` (e.g. `/[Ll]ibrary/` --> `[Ll]ibrary/`) if your project directory isn't in the same directory as your `.gitignore`.
* Unity `.scene` and `.prefab` files don't merge nicely, so make sure only one person is editing them at a time. Otherwise, you will have to deal with merge conflicts.

## Android/iOS testing and Bluetooth controller
* In [Unity Hub](https://unity.com/download) when you're installing the latest LTS version of Unity, make sure to add modules for other platforms (e.g. Android, iOS, etc.). You can alternatively install an older version (e.g. 2019) since some features (e.g. WebVR) have become deprecated in newer versions.
* For testing on Android/iOS phones without having to build/export your project each time, I recommend using the [Unity Remote](https://docs.unity3d.com/Manual/UnityRemote5.html) app which can be downloaded from your respective app store. Connect your phone via cable to your computer (Note: iOS requires also having iTunes installed) and have both the app and Unity editor open (if not detected, you have to replug or restart editor). In Unity, navigate to "Edit > Project Settings > Editor" and select your device in the Unity Remote section. When you press play in the Editor, you should also see the scene on your phone and receive various inputs from your mobile device.
* A list of possible sensor inputs for Android or iOS can be found [here](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Sensors.html). Generally, you'll want to use the [Gyroscope](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Sensors.html#gyroscope) and [AttitudeSensor](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Sensors.html#attitudesensor). Note: Gyroscope returns a `Vector3`, while AttitudeSensor returns a `Quaternion`.
* If you are using the gyroscope and attitudeSensor, you have to do some Quaternion math to rotate them correctly in Unity. A formula for this can be found [here](https://gamedev.stackexchange.com/questions/174107/unity-gyroscope-orientation-attitude-wrong).
* To build your project to iOS, you MUST have [Xcode](https://developer.apple.com/xcode/), which is only available on MacOS. Thus, you cannot build to an iPhone/iPad/iOS device unless you have a Mac (or an emulator at your own risk). Here's a [tutorial](https://www.youtube.com/watch?v=-Hr4-XNCf8Y) for building to Apple for testing.
* For Android, there shouldn't be a restriction. Here's a [tutorial](https://www.youtube.com/watch?v=Nb62z3J4A_A) for building for Android for testing.
* You can try to use the [Google Cardboard SDK](https://github.com/googlevr/cardboard-xr-plugin), but it was recently updated from the older GVR version, and there's not many tutorials about the new version. You also have to build the project in order to test it as Unity Remote won't work.
### The ShineCon Bluetooth Controller
* The ShineCon Bluetooth controller has different behavior between Android and iOS devices. When I paired it to my iPhone, it didn't do anything in VR mode besides change the Volume +/-. When you put it into Music mode on iOS (@+B) it allows you to control music Prev/Next Song with volume controls, but only within the music app. Since it's not "Made for Apple", it's not considered a controller, even though the instruction manual describes it as an "iCade" controller. I observed that it functions like key inputs on iOS when in VR Mode:
    * Up Stick w/e
    * Down Stick x/z
    * Right Stick d/c
    * Left Stick a/q
    * @ button o/g
    * Power button l/v
    * A - volume down
    * X - volume up
    * B - u/f
    * Y - j/n
    * Bumper h/g
* I found the controller can only connect to iOS or Android devices via Bluetooth. I tried to connect it directly to my laptop, but got a 'Driver Error'.
* Unity Remote did not register the controller input, so you may have to build the project directly to the phone for testing.
## WebXR/VR for Unity
* In general, the [WebVR API](https://developer.mozilla.org/en-US/docs/Web/API/WebVR_API) is no longer supported and is deprecated. It might still work on some older browsers and devices, but it's unlikely to work on most modern browsers. Also a lot of the packages used are now deprecated and work best on Unity 2019.
* The replacment [WebXR API](https://developer.mozilla.org/en-US/docs/Web/API/WebXR_Device_API) is the newer version, but it's still pretty experimental. I haven't had much luck getting it to work on my iPhone or laptop, but it seems Android devices perform the best. You might need to use an older version of Unity. You also need to have the WebGL module installed.
* [WebXR Exporter by Mozilla Firefox](https://github.com/MozillaReality/unity-webxr-export) is deprecated and not officially supported by Unity, so there is no guarantee of it working properly for certain platforms/devices or future versions of Unity. Instead there's the [De-Panther WebXR](https://github.com/De-Panther/unity-webxr-export) package which is a basically a maintained version of the Firefox WebXR. It can be used to make WebXR experiences similar to [A-Frame](https://aframe.io/docs/1.4.0/introduction/) using WebXR API. Although, I found it to be quite buggy and laggy on my browser. Moreover, it seems iOS devices are not supported. An (outdated) tutorial can be found [here](https://www.youtube.com/watch?v=ck4MDy1pUoQ).
* If you're getting an error, you need to make sure to go to 'Edit > Project Settings > Player > WebGL > Other Settings > Rendering > Color Space' and set Color Space to "Linear" with the Auto Graphics API unchecked. Alternatively, you can use "Gamma" Color space with Auto Graphics API still checked, but it tends to perform worse.
## Finding Assets
* The [Unity Asset Store](https://assetstore.unity.com/) has a bunch of free 3D assets. A lot of them even come ready-made with animations and scripts, for example this [gun asset](https://assetstore.unity.com/packages/3d/props/guns/modern-guns-handgun-129821). Just make sure it is VR compatible and not too graphics heavy.
* [SketchFab](https://sketchfab.com/3d-models?date=week&features=downloadable&sort_by=-likeCount) also has some free .fbx 3D models, but it's less likely that they will be compatible with Unity.

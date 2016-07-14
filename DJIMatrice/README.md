#DJI Matrice 100 

<img src="http://blogrepo.blob.core.windows.net/images/img_drone.png" alt="Matrice" />

##What is the Matrice 100?  
Is a drone Development platform developed by DJI - You can program 

##What can I do  with it? 
You can develop Mobile Apps using the DJI MobileSDK that DJI has as part of its development platform (and works also for their other models Phantom 3,4 and Inspire) but what really makes the Matrice interesting is the possibility to connect an onboard device using one of its UART port and talk with the autopilot. Here is where the OnboardSDK come to place. 
Finally, you can add to the Matrice an object avoidance system: The Guidance, it also has al SDK. 

##Main Site: 
https://developer.dji.com/ 

##DJI Main Repo with Instalation Guides and code samples:
https://github.com/dji-sdk 

##Mobile SDK:  
https://github.com/dji-sdk/Mobile-SDK-Android 

## Y also recommend you to read the FQA (You should)
https://github.com/dji-sdk/Onboard-SDK/blob/3.1/doc/en/FAQ_en.md#5-what-does-onboard-device-mean 

#Scenario 1: Android Server exposing the SDK Services so you can control and get Telemetry and VideoFeed from the drone
You can consume this service by any device or app externally. IoT scenarios can be very suitable for this. 

##You need to install:
[Android Studio] (https://developer.android.com/studio/index.html) and the last version of JDK.
DJI Android Mobile SDK 
##Swing by:
[The documentation that DJI released] (https://developer.dji.com/mobile-sdk/documentation/introduction/index). 
Great sample codes are in [their Github repo] (https://github.com/dji-sdk/Mobile-SDK-Android) 
 












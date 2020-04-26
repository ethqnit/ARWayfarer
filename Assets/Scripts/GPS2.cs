using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPS2 : MonoBehaviour

{
    public bool gpsstart;
    public Text speedText;
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved

            Update();
            // Stop service if there is no need to query location updates continuously
          
        }
        Input.location.Stop();
    }
    void Update()
    {
        Input.location.Start();
        speedText.text = "Location: " + Input.location.lastData.latitude + "\n Lat: " + Input.location.lastData.longitude + "\n Long:  " + Input.location.lastData.altitude + "\n Alt: " + Input.location.lastData.horizontalAccuracy + "\n Accc: ";

    }
}
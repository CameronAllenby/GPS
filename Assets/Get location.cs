using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class Getlocation : MonoBehaviour
{
    public float latitude;
    public float longitude;

    bool gpsok = false;
    public TextMeshProUGUI texttext;

    IEnumerator Start()
    {
        //checks if GPS is enabled on the device
        if (!Input.location.isEnabledByUser)
        {
            texttext.text = ("Location is not enabled by the user");
        }
        //starts GPS
        Input.location.Start();
        

        //waits until initialisation
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            texttext.text = "Failed to initialize location services";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            texttext.text = "Unable to determine location";
            yield break;
        }
        else
        {
            texttext.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude;


            gpsok = true;
        }
      
    }
    void Update()
    {
        if (gpsok)
        {

            texttext.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + "\nLon: " + Input.location.lastData.longitude
                + "\nH_Acc: " + Input.location.lastData.horizontalAccuracy;
        }
    }
}

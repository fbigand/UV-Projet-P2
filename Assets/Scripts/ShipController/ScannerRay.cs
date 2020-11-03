using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerRay : ScriptableObject
{
    private ZoneScanRay leftZone;
    private ZoneScanRay frontZone;
    private ZoneScanRay rightZone;

    private float sizeAngleZoneRadian;
    private float angleStartRadian;

    public ScannerRay(float angleWatchedRadian)
    {
        this.angleStartRadian = 0 - angleWatchedRadian / 2;
        this.sizeAngleZoneRadian = angleWatchedRadian/3;
        leftZone = new ZoneScanRay((x) => -0.5f * x + 4);
        rightZone = new ZoneScanRay((x) => -0.5f * x + 4);
        frontZone = new ZoneScanRay((x) => x*0.1f);
    }

    public void AddRay(RaycastHit rayToAdd, float rayAngleRadian)
    {
        if(rayAngleRadian < angleStartRadian+sizeAngleZoneRadian)
        {
            leftZone.AddRay(rayToAdd);
        }else if(rayAngleRadian < angleStartRadian + 2 * sizeAngleZoneRadian)
        {
            frontZone.AddRay(rayToAdd);
        }
        else
        {
            rightZone.AddRay(rayToAdd);
        }
    }

    public int takeDecision()
    {
        return 0;
    }

}

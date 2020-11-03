using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScannerRay
{
    private ZoneScanRay leftZone;
    private ZoneScanRay frontZone;
    private ZoneScanRay rightZone;
    private List<ZoneScanRay> listZones;

    private float sizeAngleZoneRadian;
    private float angleStartRadian;

    public ScannerRay(float angleWatchedRadian)
    {
        listZones = new List<ZoneScanRay>();
        this.angleStartRadian = 0 - angleWatchedRadian / 2;
        this.sizeAngleZoneRadian = angleWatchedRadian/3;
        leftZone = new ZoneScanRay(ZoneScanRay.computeRaySide,-1);
        rightZone = new ZoneScanRay(ZoneScanRay.computeRaySide, 1);
        frontZone = new ZoneScanRay(ZoneScanRay.computeRayFront, 0);
        listZones.Add(leftZone);
        listZones.Add(frontZone);
        listZones.Add(rightZone);
    }

    public void AddRay(RaycastHit2D rayToAdd, float rayAngleRadian)
    {
        
        if (rayAngleRadian < 0)
        {
            rightZone.AddRay(rayToAdd,rayAngleRadian);
        }
        else
        {
            leftZone.AddRay(rayToAdd, rayAngleRadian);
        }

        if (rayAngleRadian >= angleStartRadian + sizeAngleZoneRadian && rayAngleRadian <= angleStartRadian + 2 * sizeAngleZoneRadian)
        {
            frontZone.AddRay(rayToAdd, rayAngleRadian);
        }
       
    }

    public ZoneScanRay SafestZone()
    {
        float lessDanger = listZones.Min(zone => zone.GetValueZone());
        return listZones.Find(zone => zone.GetValueZone() == lessDanger);
    }

    public void Clear()
    {
        foreach(ZoneScanRay zone in listZones)
        {
            zone.Clear();
        }
    }

}

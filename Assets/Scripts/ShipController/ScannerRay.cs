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

    private float sizeFrontZone;
    private float angleStartRadian;

    public ScannerRay(float angleWatchedRadian)
    {
        listZones = new List<ZoneScanRay>();
        this.angleStartRadian = - angleWatchedRadian / 3;
        this.sizeFrontZone = angleWatchedRadian*5/9;
        leftZone = new ZoneScanRay(ZoneScanRay.computeRaySide,-1);
        rightZone = new ZoneScanRay(ZoneScanRay.computeRaySide, 1);
        frontZone = new ZoneScanRay(ZoneScanRay.computeRayFront, 0);
        listZones.Add(leftZone);
        listZones.Add(frontZone);
        listZones.Add(rightZone);
    }

    public void AddRay(RaycastHit2D rayToAdd, float rayAngleRadian)
    {
        
        if (rayAngleRadian < -sizeFrontZone/2)
        {
            rightZone.AddRay(rayToAdd,rayAngleRadian);
        }
        else if(rayAngleRadian > sizeFrontZone / 2)
        {
            leftZone.AddRay(rayToAdd, rayAngleRadian);
        }

        else 
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

    public RaycastHit2D findClosest()
    {
        RaycastHit2D closest = leftZone.findClosest();
        foreach (ZoneScanRay zone in listZones)
        {
            RaycastHit2D currentZoneClosest = zone.findClosest();
            if (currentZoneClosest.distance < closest.distance)
            {
                closest = currentZoneClosest;
            }
        }

        return closest;
    }

}

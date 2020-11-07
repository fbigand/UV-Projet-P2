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

    public ScannerRay(float angleWatchedRadian)
    {
        listZones = new List<ZoneScanRay>();
        this.sizeFrontZone = angleWatchedRadian*5/9;
        leftZone = new ZoneScanRay(ZoneScanRay.ComputeRaySide,-1);
        rightZone = new ZoneScanRay(ZoneScanRay.ComputeRaySide, 1);
        frontZone = new ZoneScanRay(ZoneScanRay.ComputeRayFront, 0);
        listZones.Add(leftZone);
        listZones.Add(frontZone);
        listZones.Add(rightZone);
    }

    public void AddRay(RaycastHit2D rayToAdd, float rayAngleRadian)
    {
        
        if (rayAngleRadian < -sizeFrontZone / 2)
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
        if (frontZone.GetValueZone()==0)
        {
            return frontZone;
        }

        float lessDanger = listZones.Min(zone => zone.GetValueZone());
        return listZones.Find(zone => zone.GetValueZone() == lessDanger);
    }

    public bool isEnnemyInFrontZone()
    {
        return frontZone.isFrontOfEnnemy;
    }

    public void Clear()
    {
        foreach(ZoneScanRay zone in listZones)
        {
            zone.Clear();
        }
    }

    
}

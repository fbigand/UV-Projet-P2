using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScannerRay : ScriptableObject
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
        leftZone = new ZoneScanRay((x) => -0.5f * x + 4,-1);
        rightZone = new ZoneScanRay((x) => -0.5f * x + 4,1);
        frontZone = new ZoneScanRay((x) => x*0.1f,0);
        listZones.Add(leftZone);
        listZones.Add(frontZone);
        listZones.Add(rightZone);
    }

    public void AddRay(RaycastHit2D rayToAdd, float rayAngleRadian)
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
        float lessDanger = listZones.Min(zone => zone.GetValueZone());
        return listZones.Find(zone => zone.GetValueZone() == lessDanger).associatedDecision;
    }

    public void Clear()
    {
        foreach(ZoneScanRay zone in listZones)
        {
            zone.Clear();
        }
    }

}

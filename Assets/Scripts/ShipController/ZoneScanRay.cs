using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScanRay : ScriptableObject
{
    // Start is called before the first frame update
    
    private Func<float,float> fonctionDistance;
    private Func<Vector2,float> fonctionNormale;
    private float poidsTotal;

    private List<RaycastHit> listRays;

    public ZoneScanRay(Func<float, float> fonctionDistance)
    {
        listRays = new List<RaycastHit>();
        this.fonctionDistance = fonctionDistance;
        //this.fonctionNormale = fonctionNormale;
    }

    public void AddRay(RaycastHit rayToAdd)
    {
        listRays.Add(rayToAdd);
    }

    public float GetValueZone()
    {
        if(poidsTotal != 0)
        {
            return poidsTotal;
        }else
        {
            return Compute();
        }
    }

    private float Compute()
    {
        poidsTotal = 0;
        foreach(RaycastHit ray in listRays)
        {
            poidsTotal += fonctionDistance.Invoke(ray.distance);
            //poidsTotal += fonctionNormale.Invoke(ray.normal);
        }
        return poidsTotal;
    }
}

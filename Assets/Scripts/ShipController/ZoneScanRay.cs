using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScanRay
{
    // Start is called before the first frame update
    
    private Func<float,float> fonctionDistance;
    private Func<Vector2,float> fonctionNormale;
    private float poidsTotal;
    public int associatedDecision;

    private List<RaycastHit2D> listRays;

    public ZoneScanRay(Func<float, float> fonctionDistance, int associatedDecision)
    {
        this.associatedDecision = associatedDecision;
        listRays = new List<RaycastHit2D>();
        this.fonctionDistance = fonctionDistance;
        //this.fonctionNormale = fonctionNormale;
    }

    public void AddRay(RaycastHit2D rayToAdd)
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
        foreach(RaycastHit2D ray in listRays)
        {
            poidsTotal += fonctionDistance.Invoke(ray.distance);
            //poidsTotal += fonctionNormale.Invoke(ray.normal);
        }
        return poidsTotal;
    }

    public void Clear()
    {
        listRays.Clear();
        poidsTotal = 0;
    }
}

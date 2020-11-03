using Packages.Rider.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScanRay
{
    // Start is called before the first frame update

    private Func<StoreRay, float> fonctionDistance;
    public float danger;
    public int associatedDecision;

    private List<StoreRay> listRays;

    public ZoneScanRay(Func<StoreRay, float> fonctionDistance, int associatedDecision)
    {
        this.associatedDecision = associatedDecision;
        listRays = new List<StoreRay>();
        this.fonctionDistance = fonctionDistance;
    }

    public void AddRay(RaycastHit2D rayToAdd, float angle)
    {
        listRays.Add(new StoreRay(rayToAdd,angle));
    }

    public float GetValueZone()
    {
        if (danger != 0)
        {
            return danger;
        } else
        {
            return Compute();
        }
    }

    private float Compute()
    {
        danger = 0;
        foreach (StoreRay ray in listRays)
        {
            danger += fonctionDistance.Invoke(ray);
        }
        return danger;
    }

    public void Clear()
    {
        listRays.Clear();
        danger = 0;
    }

    public static float computeRayFront(StoreRay ray)
    {
        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;
        float importanceRay = Mathf.Clamp(-4 * Mathf.Abs(ray.angle) - 3, 1, 3);

        float danger = 4f * Mathf.Clamp((-3f * x + 5) / (x * 8f), 0, 50);

        //plus l'angle est droit devant plus ça donne des points danger
       
        return danger;
    }

    public static float computeRaySide(StoreRay ray)
    {
        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;
        float danger = Mathf.Clamp((-0.8f * x + 4) / (x * 10f), 0f, 50f);

        //plus l'angle est droit devant plus ça donne des points danger
        /*x = Mathf.Abs(ray.angle);
        float facteur = Mathf.Clamp(-4 * x - 3, 1, 3);

        danger *= facteur;*/

        return danger;
    }

    public class StoreRay
    {
        public RaycastHit2D ray;
        public float angle;

        public StoreRay(RaycastHit2D ray, float angle)
        {
            this.ray = ray;
            this.angle = angle;
        }

    }
}

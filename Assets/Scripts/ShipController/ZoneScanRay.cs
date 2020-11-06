using Packages.Rider.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Pour l'ia a détection de zone de danger
//Cette classe représente une zone
public class ZoneScanRay
{
    //la fonction qui sera utilisée pour déterminer le danger d'un raycast
    private Func<StoreRay, float> functionDetermineDangerRay;
    //contient la valeur de danger de la zone
    public float danger;
    //La direction apprendre associé à la zone (-1 gauche, 1 droite, 0 tout droit)
    public int decision;

    //la liste des rayons de cette zone
    private List<StoreRay> listRays;

    public ZoneScanRay(Func<StoreRay, float> fonctionDistance, int associatedDecision)
    {
        this.decision = associatedDecision;
        listRays = new List<StoreRay>();
        this.functionDetermineDangerRay = fonctionDistance;
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

    //on calcule le danger de la zone
    private float Compute()
    {
        danger = 0;
        foreach (StoreRay ray in listRays)
        {
            danger += functionDetermineDangerRay.Invoke(ray);
        }
        return danger;
    }

    public void Clear()
    {
        listRays.Clear();
        danger = 0;
    }

    //une methode pour calculer le danger des rayons d'une zone devant
    public static float ComputeRayFront(StoreRay ray)
    {
        if (ray.ray.collider.CompareTag("PickUp"))
        {
            return 0;
        }

        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;
        float importanceRay = Mathf.Clamp(-36 * Mathf.Abs(ray.angle) +10, 1, 10);
        float danger = (3f+importanceRay) * Mathf.Clamp((-3f * x + 6) / (x * 8f), 0, 50);

       
        return danger;
    }


    // une methode pour calculer les rayons d'une zone latérale
    public static float ComputeRaySide(StoreRay ray)
    {
        if (ray.ray.collider.CompareTag("PickUp"))
        {
            return 0;
        }

        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;
        float danger = Mathf.Clamp((-0.8f * x + 4f) / (x * 10f), 0f, 50f);

        //plus l'angle est droit devant plus ça donne des points danger
        /*x = Mathf.Abs(ray.angle);
        float facteur = Mathf.Clamp(-4 * x - 3, 1, 3);

        danger *= facteur;*/

        return danger;
    }

    //une classe qui représent un rayon et l'angle de ce rayon par rapport au vaisseau
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

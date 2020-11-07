using Packages.Rider.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

//Pour l'ia a détection de zone de danger
//Cette classe représente une zone
public class ZoneScanRay
{
    public bool isFrontOfEnnemy = false;
    //la fonction qui sera utilisée pour déterminer le danger d'un raycast
    private Func<StoreRay,ZoneScanRay, float> functionDetermineDangerRay;
    //contient la valeur de danger de la zone
    public float danger;
    //La direction apprendre associé à la zone (-1 gauche, 1 droite, 0 tout droit)
    public int decision;

    //la liste des rayons de cette zone
    private List<StoreRay> listRays;

    public ZoneScanRay(Func<StoreRay, ZoneScanRay, float> fonctionDistance, int associatedDecision)
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
            danger += functionDetermineDangerRay.Invoke(ray,this);
        }
        return danger;
    }

    public void Clear()
    {
        listRays.Clear();
        isFrontOfEnnemy = false;
        danger = 0;
    }

    //une methode pour calculer le danger des rayons d'une zone devant
    public static float ComputeRayFront(StoreRay ray, ZoneScanRay zone)
    {
        float danger = 0;
        if (ray.ray.collider.CompareTag("PickUp"))
        {
            return 0;
        }
        else if (ray.ray.collider.CompareTag("Player")&&ray.ray.distance < 3 && Mathf.Abs(ray.angle)<0.1f )
        {
            zone.isFrontOfEnnemy = true;
        }

        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;
        float importanceRay = Mathf.Clamp(-36f * Mathf.Abs(ray.angle) +10f, 1, 10);
        float porteeRay = Mathf.Clamp(-1.33f*Mathf.Abs(ray.angle)+2, 0, 2);
        danger += (importanceRay+3f) * Mathf.Clamp((-1.3f * x + 4+porteeRay) / (x * 5f), 0, 50);

       
        return danger;
    }


    // une methode pour calculer les rayons d'une zone latérale
    public static float ComputeRaySide(StoreRay ray, ZoneScanRay zone)
    {
        if (ray.ray.collider.CompareTag("PickUp"))
        {
            return 0;
        }

        // plus c'est proche plus ça donne des points de danger
        float x = ray.ray.distance;

        //float angleImportance = Mathf.Clamp(Mathf.Abs(ray.angle), 0, 1.5f);
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

using System;

using UnityEngine;

//la classe qui décrit le comportement d'une IA à détection de zone de danger
public class ControllerIAMediumHard : ControllerIA
{
    private Transform raycastStartPosition;
    public int raycastNumber;
    public int distanceCaptureRay;
    private ScannerRay scannerRay;
    private float angleScannedRadian = (float)Math.PI;
    private bool useSecondaryBonus = false;
    private bool usePrimaryBonus = false;

    void Start()
    {
        scannerRay = new ScannerRay(angleScannedRadian);
        raycastStartPosition = gameObject.transform.Find("HotSpotFront");
    }

    public override float GetRotation()
    {
        scannerRay.Clear();

        //on execute le scan des 3 zones autour de nous avec les raycasts
        float angleStep = angleScannedRadian / (raycastNumber - 1);
        int nbrRayCasted = 0;
        for (float angle = -angleScannedRadian / 2; nbrRayCasted < raycastNumber; angle += angleStep)
        {
            Vector2 translatedVector = Trigonometry.RotateVector(transform.up, angle);

            RaycastHit2D[] result = new RaycastHit2D[1];
            Physics2D.Raycast(
                origin: raycastStartPosition.transform.position,
                direction: translatedVector,
                contactFilter: new ContactFilter2D().NoFilter(),
                results: result,
                distance: distanceCaptureRay
            );
            nbrRayCasted++;

            if (result[0].collider.CompareTag("Player"))
            {
                Debug.DrawRay(raycastStartPosition.transform.position, translatedVector, Color.green);
            }
            scannerRay.AddRay(result[0], angle);
        }

        //on regarde quelle zone présente un danger le plus faible
        ZoneScanRay zoneDecision = scannerRay.SafestZone();

        //si la zone choisie est tout de même très dangereuse
        if (zoneDecision.danger >= 500)
        {
            //alors on utilise le pouvoir de saut
            useSecondaryBonus = true;
        }

        if (scannerRay.isEnnemyInFrontZone())
        {
            usePrimaryBonus = true;
        }

        print(zoneDecision.decision);
        return zoneDecision.decision;

    }

    public override bool IsUsingPrimaryBonus()
    {
        bool ret = usePrimaryBonus;
        usePrimaryBonus = false;
        return ret;
    }

    public override bool IsUsingSecondaryBonus()
    {
        if (useSecondaryBonus)
        {
            useSecondaryBonus = false;
            return true;
        }
        else
        {
            return false;
        }
    }

}

using System;

using UnityEngine;

public class ControllerIAMediumHard : ControllerIA
{
    private Transform raycastStartPosition;
    public int raycastNumber;
    public int distanceCaptureRay;
    private ScannerRay scannerRay;
    private float angleScannedRadian = (float)Math.PI;
    private bool isInDanger = false;

    void Start()
    {
        scannerRay = new ScannerRay(angleScannedRadian);
        raycastStartPosition = gameObject.transform.Find("HotSpotFront");
    }

    public override float GetRotation()
    {
        scannerRay.Clear();
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

            scannerRay.AddRay(result[0],angle);
        }

        ZoneScanRay zoneDecision = scannerRay.SafestZone();
        if (zoneDecision.danger >= 170)
        {
            print(zoneDecision.danger);
            isInDanger = true;
        }
        return zoneDecision.associatedDecision;
        
    }

    public override bool IsUsingPrimaryBonus()
    {
        return false;
    }

    public override bool IsUsingSecondaryBonus()
    {
        if (isInDanger)
        {
            isInDanger = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerAlgo : ControllerAbstract
{
    private Transform raycastStartPosition;
    public int raycastNumber;

    private void Start()
    {
        raycastStartPosition = gameObject.transform.Find("HotSpotFront");
    }

    public override float GetRotation()
    {

        List<RaycastHit2D[]> raycastList = new List<RaycastHit2D[]>();
        float angleStep = Mathf.PI / (raycastNumber - 1);
        for (float angle = Mathf.PI / 2; raycastList.Count < raycastNumber; angle -= angleStep)
        {
            Vector2 translatedVector = Trigonometry.VectorTranslatedByAngle(transform.up, angle);

            RaycastHit2D[] result = new RaycastHit2D[1];
            Physics2D.Raycast(
                origin: raycastStartPosition.transform.position,
                direction: translatedVector,
                contactFilter: new ContactFilter2D().NoFilter(),
                results: result,
                distance: 20
            );
            raycastList.Add(result);
        }

        RaycastHit2D closest = raycastList.First()[0];
        foreach (RaycastHit2D[] raycastArr in raycastList)
        {
            if (raycastArr[0].distance < closest.distance)
            {
                closest = raycastArr[0];
            }
        }

        float detectionDistance = 0.4f;

        if (closest.distance > detectionDistance)
        {
            return KeepForward();
        }
        else
        {
            Vector2 recommendedTrajectory = (Vector2)transform.up + closest.normal;
            float angleTrajectory = Vector2.SignedAngle(transform.up, recommendedTrajectory);
            if (angleTrajectory < 0)
            {
                return TurnRight();
            }
            else
            {
                return TurnLeft();
            }
        }
    }

    public override bool IsUsingPrimaryBonus()
    {
        return false;
    }

    public override bool IsUsingSecondaryBonus()
    {
        return false;
    }
}

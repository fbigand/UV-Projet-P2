using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ControllerAlgo : ControllerAbstract
{
    public Transform raycastStartPosition;
    public int raycastNumber;
    public override float GetRotation()
    {
        // Get vector transform.up translated by an angle in radians
        Vector2 TranslateTransformUpByAngle(float radians)
        {
            float vectorAngle = Mathf.Atan(transform.up.y / transform.up.x) + radians;
            return new Vector2(
                Mathf.Cos(vectorAngle),
                Mathf.Sin(vectorAngle)
            );
        }

        Debug.DrawRay(start: raycastStartPosition.position, dir: transform.up, color: Color.green, duration: Time.deltaTime);

        List<RaycastHit2D[]> raycastList = new List<RaycastHit2D[]>();
        float angleStep = Mathf.PI / (raycastNumber - 1);
        for (float angle = Mathf.PI / 2; angle >= -Mathf.PI / 2; angle -= angleStep)
        {
            Vector2 translatedVector = TranslateTransformUpByAngle(angle);
            Debug.DrawRay(start: raycastStartPosition.position, dir: translatedVector, color: Color.red, duration: Time.deltaTime);

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

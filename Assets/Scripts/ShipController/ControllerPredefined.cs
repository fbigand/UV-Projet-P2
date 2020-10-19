using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class ControllerPredefined : ControllerAbstract
{
    private Vector3 startPosition;
    private Stopwatch time = new Stopwatch();
    private double rotationFactor = 4000; // empirical value

    private void Start()
    {
        startPosition = transform.position;
        time.Start();
    }

    public override float GetRotation()
    {
        Vector3 currentPosition = transform.position;
        float distanceFromStart = Vector3.Distance(startPosition, currentPosition);
        double maxDistance = math.sqrt(time.Elapsed.TotalMilliseconds / rotationFactor);
        return distanceFromStart > maxDistance ? TurnLeft() : TurnRight();
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

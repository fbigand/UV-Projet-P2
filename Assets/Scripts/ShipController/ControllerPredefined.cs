using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class ControllerPredefined : MonoBehaviour, IController
{
    private Vector3 startPosition;
    private Stopwatch time = new Stopwatch();
    private double rotationFactor = 3680; // empirical value

    private void Start()
    {
        startPosition = transform.position;
        time.Start();
    }

    float IController.GetRotation()
    {
        Vector3 currentPosition = transform.position;
        float distanceFromStart = Vector3.Distance(startPosition, currentPosition);
        double maxDistance = math.sqrt(time.Elapsed.TotalMilliseconds / rotationFactor);

        return distanceFromStart > maxDistance ? -1f : 0f;
    }

    bool IController.IsUsingPrimaryBonus()
    {
        return false;
    }

    bool IController.IsUsingSecondaryBonus()
    {
        return false;
    }
}

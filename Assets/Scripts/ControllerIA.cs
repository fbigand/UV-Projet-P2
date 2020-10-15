using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIA : Controller
{
    
    override public float GetRotation()
    {
        return Random.Range(-1f, 1f);
    }

    override public bool IsUsingPrimaryBonus()
    {
        return false;
    }

    override public bool IsUsingSecondaryBonus()
    {
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : Controller
{
    public string moveAxis;
    public string attackAxis;

    //return -1 for left, 1 for right and 0 equals forward
    override public float GetRotation()
    {
        return Input.GetAxis(moveAxis);
    }

    override public bool IsUsingPrimaryBonus()
    {
        return Input.GetAxis(attackAxis)>0;
    }

    override public bool IsUsingSecondaryBonus()
    {
        return Input.GetAxis(attackAxis) < 0;
    }

}

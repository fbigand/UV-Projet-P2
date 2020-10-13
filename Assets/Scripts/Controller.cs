using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string moveAxis;
    public string attackAxis;

    //return -1 for left, 1 for right and 0 equals forward
    public float GetRotation()
    {
        return Input.GetAxis(moveAxis);
    }

    public bool IsAttacking()
    {
        return Input.GetAxis(attackAxis)>0;
    }

}

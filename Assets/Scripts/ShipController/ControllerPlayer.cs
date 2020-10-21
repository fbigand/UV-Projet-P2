using UnityEngine;

public class ControllerPlayer : MonoBehaviour, IController
{
    public string moveAxis;
    public string attackAxis;

    //return -1 for left, 1 for right and 0 equals forward
    virtual public float GetRotation()
    {
        return Input.GetAxis(moveAxis);
    }

    public bool IsUsingPrimaryBonus()
    {
        return Input.GetAxis(attackAxis) > 0;
    }

    public bool IsUsingSecondaryBonus()
    {
        return Input.GetAxis(attackAxis) < 0;
    }
}

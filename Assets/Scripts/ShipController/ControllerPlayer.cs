using UnityEngine;

public class ControllerPlayer : MonoBehaviour, IController
{
    public string moveAxis;
    public string attackAxis;

    //return -1 for left, 1 for right and 0 equals forward
    float IController.GetRotation()
    {
        return Input.GetAxis(moveAxis);
    }

    bool IController.IsUsingPrimaryBonus()
    {
        return Input.GetAxis(attackAxis) > 0;
    }

    bool IController.IsUsingSecondaryBonus()
    {
        return Input.GetAxis(attackAxis) < 0;
    }
}

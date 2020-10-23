using UnityEngine;

public abstract class ControllerAbstract : MonoBehaviour, IController
{
    protected float TurnLeft()
    {
        return -1f;
    }

    protected float TurnRight()
    {
        return 1f;
    }

    protected float KeepForward()
    {
        return 0f;
    }

    public abstract float GetRotation();
    public abstract bool IsUsingPrimaryBonus();
    public abstract bool IsUsingSecondaryBonus();
}

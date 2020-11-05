using UnityEngine;

public class ControllerRandom : ControllerIA
{
    public override float GetRotation()
    {
        float res = Random.Range(0, 3);
        if (res == 2)
        {
            return TurnLeft();

        }
        else if (res == 1)
        {
            return TurnRight();
        }
        else
        {
            return KeepForward();
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

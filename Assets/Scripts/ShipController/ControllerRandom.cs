using UnityEngine;

public class ControllerRandom : MonoBehaviour, IController
{
    float IController.GetRotation()
    {
        return Random.Range(-1f, 1f);
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

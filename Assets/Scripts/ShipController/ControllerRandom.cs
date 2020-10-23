using UnityEngine;

public class ControllerRandom : MonoBehaviour, IController
{
    float IController.GetRotation()
    {
        float res = Random.Range(0,3);
        if(res == 2)
        {
            res = 1;

        }else if (res == 1)
        {
            res = -1;
        }
        return  res ;
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

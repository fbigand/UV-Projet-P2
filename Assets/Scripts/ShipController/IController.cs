using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    float GetRotation();
    bool IsUsingPrimaryBonus();
    bool IsUsingSecondaryBonus();
}

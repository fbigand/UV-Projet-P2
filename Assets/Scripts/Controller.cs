using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    abstract public float GetRotation();

    abstract public bool IsUsingPrimaryBonus();

    abstract public bool IsUsingSecondaryBonus();
}

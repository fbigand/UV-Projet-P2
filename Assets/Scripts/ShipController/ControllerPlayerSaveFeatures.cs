using UnityEngine;

public class ControllerPlayerSaveFeatures : ControllerPlayer
{
    //return -1 for left, 1 for right and 0 equals forward
    override public float GetRotation()
    {
        float input = Input.GetAxis(moveAxis);
        if (input != 0)
        {
            input = 1f*Mathf.Sign(input);
        }

        DataWriter.instance.writeDecisionPlayer(input);
        DataWriter.instance.writePos(transform.position);

        return input;
    }
}

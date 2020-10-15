using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotationAngle = 1f;

    private Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float translationY = speed;

        float rotationZ = controller.GetRotation();

        //The player want to turn
        if(rotationZ != 0)
        {
            //On inverse le signe pour avoir un sens de rotation cohérent
            rotationZ = -0.5f * Mathf.Sign(rotationZ) ;
        }

        transform.Rotate(0f, 0f, rotationZ * rotationAngle );
        transform.Translate(0f, translationY , 0f);
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotationAngle = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translationY = speed;

        float rotationZ = Input.GetAxis("Horizontal");

        //The player want to turn
        if(rotationZ != 0)
        {
            //On inverse le signe pour avoir un sens de rotation cohérent
            rotationZ = -0.5f * Mathf.Sign(rotationZ) ;
        }

        transform.Rotate(0f, 0f, rotationZ*rotationAngle);
        transform.Translate(0f, translationY, 0f);
    }

    // TODO: Delete this function to keep movement according to rotation instead
    private void simpleMoveWithArrows()
    {
        float translationY = Input.GetAxis("Vertical") * speed;
        float translationX = Input.GetAxis("Horizontal") * speed;

        translationY *= Time.deltaTime;
        translationX *= Time.deltaTime;

        transform.Translate(translationX, translationY, 0f);
    }
}

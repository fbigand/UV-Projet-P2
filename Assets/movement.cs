using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translationY = Input.GetAxis("Vertical") * speed;
        float translationX = Input.GetAxis("Horizontal") * speed;

        translationY *= Time.deltaTime;
        translationX *= Time.deltaTime;

        transform.Translate(translationX, translationY, 0f);

        //transform.Rotate(0, rotation, 0);
    }
}

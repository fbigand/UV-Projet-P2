using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersAttack : MonoBehaviour
{ 
    //Point de tir
    public Transform shootPoint;

    // Floats
    public float rocketSpeed = 1f;

    //Objets à faire spawn
    public GameObject rocketPrefab;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            SimpleRocket(true);
        }
    }

    void SimpleRocket(bool attacking)
    {
        if (attacking)
        {
            GameObject rocket = Instantiate(rocketPrefab, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
            rocket.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.forward.x,transform.forward.y) * rocketSpeed;
        }
        
    }

}

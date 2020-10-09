using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersAttack : MonoBehaviour
{ 
    public Transform shootPoint;
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
        }
        
    }

}

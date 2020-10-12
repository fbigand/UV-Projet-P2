using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PowersAttack : MonoBehaviour
{ 
    public Transform shootPoint;
    public GameObject rocketPrefab;
    public int rocketCooldownMillis; // in millis
    private Stopwatch cooldownCounter = new Stopwatch();

    private void Start()
    {
        cooldownCounter.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true 
            && cooldownCounter.Elapsed.TotalMilliseconds > rocketCooldownMillis)
        {
            LaunchRocket();
            cooldownCounter.Restart();
        }
    }

    void LaunchRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;        
    }
}

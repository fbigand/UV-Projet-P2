using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PowersAttack : MonoBehaviour
{ 
    public Transform shootPoint;
    public GameObject rocketPrefab;
    public int rocketCooldownMillis; // in millis
    private Stopwatch cooldownCounter = new Stopwatch();
    private int compteAReboursRocket;
    public Text textCDRocket;

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

        compteAReboursRocket = (rocketCooldownMillis / 1000) - cooldownCounter.Elapsed.Seconds;

        if (compteAReboursRocket <= 0)
        {
            textCDRocket.text = "Ready";
        }

        else
        {
            textCDRocket.text = compteAReboursRocket.ToString();
        }
        
    }

    void LaunchRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;        
    }
}

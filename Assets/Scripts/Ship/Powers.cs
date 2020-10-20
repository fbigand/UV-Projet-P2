using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Powers : MonoBehaviour
{ 
    public Transform shootPoint;

    private Stopwatch counterCooldownPrimary = new Stopwatch();
    private Stopwatch counterCooldownSecondary = new Stopwatch();

    public int durationCooldownPowerUp; // in millis
    public int durationCooldownPowerDown; // in millis
    

    private HudPlayer hudPlayer;

    public GameObject rocketPrefab;
    private GameObject fire;
    private Animator anim;
    private CapsuleCollider2D spaceshipCollider;
    private IController controller;

    private void Start()
    {
        counterCooldownPrimary.Start();
        counterCooldownSecondary.Start();

        hudPlayer = GetComponent<Player>().hudplayer;
        anim = gameObject.GetComponent<Animator>();
        spaceshipCollider = gameObject.GetComponent<CapsuleCollider2D>();
        controller = GetComponent<IController>();

        fire = GameObject.Find("Fire");
    }

    void Update()
    {
        int remainingCooldownPrimary = durationCooldownPowerUp - counterCooldownPrimary.Elapsed.Seconds;
        int remainingCooldownSecondary = durationCooldownPowerDown - counterCooldownSecondary.Elapsed.Seconds;

        hudPlayer.SetTextBonus(remainingCooldownPrimary.ToString(), remainingCooldownSecondary.ToString());

        if (remainingCooldownPrimary <= 0)
        {
            hudPlayer.SetPrimaryToReady();

            if (controller.IsUsingPrimaryBonus())
            {
                LaunchRocket();
                counterCooldownPrimary.Restart();
            }
        }

        if (remainingCooldownSecondary <= 0)
        {

            hudPlayer.SetSecondaryToReady();

            if (controller.IsUsingSecondaryBonus())
            {
                Jump();
                counterCooldownSecondary.Restart();
            }
        }
    }

    void LaunchRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;        
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
        anim.SetBool("OntheAir", true);
        spaceshipCollider.enabled = false;
        StartCoroutine(WaitAndReset());
        gameObject.GetComponent<Snake>().isDrawingTail = false;
        gameObject.GetComponent<Snake>().ResetLastDistance();
        fire.SetActive(false);


        IEnumerator WaitAndReset()
        {
            yield return new WaitForSeconds(GetComponent<Snake>().distanceBreakInTail* Time.fixedDeltaTime / gameObject.GetComponent<ShipMovement>().speed);
            anim.SetBool("OntheAir", false);
            spaceshipCollider.enabled = true;
            fire.SetActive(true);
            
        }
    }
}


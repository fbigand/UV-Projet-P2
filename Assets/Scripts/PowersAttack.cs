using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PowersAttack : MonoBehaviour
{ 
    public Transform shootPoint;

    private Stopwatch counterCooldownUp = new Stopwatch();
    private Stopwatch counterCooldownDown = new Stopwatch();

    public int durationCooldownPowerUp; // in millis
    public int durationCooldownPowerDown; // in millis

    public Text textCooldownUp;
    public Text textCooldownDown;

    public GameObject rocketPrefab;
    private Animator anim;
    private CapsuleCollider2D spaceshipCollider;
    private Controller controller;
    public Text textCDRocket;

    private void Start()
    {
        counterCooldownUp.Start();
        counterCooldownDown.Start();

        anim = gameObject.GetComponent<Animator>();
        spaceshipCollider = gameObject.GetComponent<CapsuleCollider2D>();
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        int remainingCooldownUp = durationCooldownPowerUp - counterCooldownUp.Elapsed.Seconds;
        int remainingCooldownDown = durationCooldownPowerDown - counterCooldownDown.Elapsed.Seconds;

        bool upIsReady = remainingCooldownUp <= 0;
        bool downIsReady = remainingCooldownDown <= 0;

        textCooldownUp.text = remainingCooldownUp.ToString();
        textCooldownDown.text = remainingCooldownDown.ToString();

        if (upIsReady)
        {
            textCooldownUp.text = "Ready";

            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                LaunchRocket();
                counterCooldownUp.Restart();
            }
        }

        if (downIsReady)
        {
            textCooldownDown.text = "Ready";

            if (Input.GetKeyDown(KeyCode.DownArrow) == true)
            {
                Jump();
                counterCooldownDown.Restart();
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
        StartCoroutine(WaitAndReset(0.5f));
        gameObject.GetComponent<Snake>().isDrawingTail = false;

        IEnumerator WaitAndReset(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            anim.SetBool("OntheAir", false);
            spaceshipCollider.enabled = true;
            gameObject.GetComponent<Snake>().createTail();
            gameObject.GetComponent<Snake>().isDrawingTail = true;
        }
    }
}


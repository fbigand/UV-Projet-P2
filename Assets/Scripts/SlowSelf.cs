using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.WSA;

public class SlowSelf : PickUps
{

    public int countTime = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShipMovement>().speed *= 0.8f;
            Destroy(gameObject);
            StartCoroutine(WaitAndReset(collision, 1.2f));
        }

        IEnumerator WaitAndReset(Collider2D collider, float resetSpeed)
        {
            int timer = 0;
            while (timer < countTime)
            {
                timer += 1;
                yield return new WaitForSeconds(1);
            }
            collider.GetComponent<ShipMovement>().speed *= resetSpeed;
        }
    }             
}

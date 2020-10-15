using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public bool slowgood = false;
    public bool slowbad = false;
    public bool fastgood = false;
    public bool fastbad = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (slowgood)
            {
                collision.GetComponent<ShipMovement>().speed *= 0.8f;
                StartCoroutine(WaitAndReset(2f));
                
            }

            if (slowbad)
            {
                
            }

            if (fastgood)
            {
                collision.GetComponent<ShipMovement>().speed *= 1.2f;
                StartCoroutine(WaitAndReset(2f));
            }

            if (fastbad)
            {

            }

            Destroy(gameObject);

        }

        IEnumerator WaitAndReset(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            collision.GetComponent<ShipMovement>().speed *= 1.2f;
        }

        
    }
}

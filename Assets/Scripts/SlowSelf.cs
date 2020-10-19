using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.WSA;

public class SlowSelf : PickUps
{

    override public void ActivatePickUp(GameObject ship)
    {
        Destroy(gameObject);
        ship.GetComponent<ShipMovement>().speed -= speedModif;
        StartCoroutine(WaitAndReset(ship.GetComponent<ShipMovement>()));
    }

    IEnumerator WaitAndReset(ShipMovement shipmov)
    {
        yield return new WaitForSeconds(3);
        shipmov.speed += speedModif;
    }             
}

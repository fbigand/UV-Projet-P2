using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastOthers : PickUps
{
    override public void ActivatePickUp(GameObject ship)
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        ship.GetComponent<ShipMovement>().speed += speedModif;
        StartCoroutine(WaitAndReset(ship.GetComponent<ShipMovement>()));
    }

    IEnumerator WaitAndReset(ShipMovement shipmov)
    {
        yield return new WaitForSeconds(5);
        shipmov.speed -= speedModif;
        Destroy(gameObject);
    }
}

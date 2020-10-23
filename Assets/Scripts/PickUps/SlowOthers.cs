using System.Collections;
using UnityEngine;

public class SlowOthers : PickUps
{
    override public void ActivatePickUp(GameObject ship)
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        foreach (var head in activePlayers)
        {
            head.GetComponent<ShipMovement>().speed -= speedModif;
        }

        ship.GetComponent<ShipMovement>().speed += speedModif;
        StartCoroutine(WaitAndReset(ship.GetComponent<ShipMovement>()));
    }

    IEnumerator WaitAndReset(ShipMovement shipmov)
    {
        yield return new WaitForSeconds(5);
        foreach (var head in activePlayers)
        {
            head.GetComponent<ShipMovement>().speed += speedModif;
        }
        shipmov.speed -= speedModif;
        Destroy(gameObject);
    }
}

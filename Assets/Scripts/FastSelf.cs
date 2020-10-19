using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSelf : PickUps
{

    private void Awake()
    {
        
    }
    override public void ActivatePickUp(GameObject ship)
    {        
        Destroy(gameObject);
        ship.GetComponent<ShipMovement>().speed += speedModif;
        StartCoroutine(WaitAndReset(ship.GetComponent<ShipMovement>()));
    }

    IEnumerator WaitAndReset(ShipMovement shipmov)
    {
        yield return new WaitForSeconds(3);
        shipmov.speed -= speedModif;
    }
}    

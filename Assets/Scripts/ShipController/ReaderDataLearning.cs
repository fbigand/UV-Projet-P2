using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ReaderDataLearning : Object
{
    private Transform transformShipPlayer;
    // Start is called before the first frame update
    private float angleDiffBetweenRaycast;
    private ManagePlayers managePlayers;
    private float angleShipFieldview;
    private int nbRaycasts;
    private Transform headPosition;
    private Player player;
    private RaycastHit2D[] result;

    public ReaderDataLearning(Transform transformShipPlayer, ManagePlayers managePlayers, float angleShipFieldview, int nbRaycasts, Transform headPosition, Player player)
    {
        this.transformShipPlayer = transformShipPlayer;
        this.managePlayers = managePlayers;
        this.angleShipFieldview = angleShipFieldview;
        this.nbRaycasts = nbRaycasts;
        this.headPosition = headPosition;
        this.player = player;
        result = new RaycastHit2D[1];
        float nbDivision = nbRaycasts > 1 ? nbRaycasts - 1 : 1;
        angleDiffBetweenRaycast = angleShipFieldview / nbDivision;
    }

    public string Read()
    {
        return SaveInfoShip(transformShipPlayer) + SaveRaycasts() + SaveOtherShipsInfo();
    }

    private string SaveInfoShip(Transform spaceship = null)
    {
        if (spaceship == null)
        {
            return "0;0;0;";
        }
        else
        {
            return spaceship.position.x + ";" + spaceship.position.y + ";" + spaceship.rotation.eulerAngles.z+";";
        }
    }

    private string SaveOtherShipsInfo()
    {
        string res = "";
        for (int i = 0; i < managePlayers.usableSpaceships.Length; i++)
        {
            if (i != player.id)
            {
               
                if (i < managePlayers.activePlayers.Length)
                {
                    res += SaveInfoShip(managePlayers.activePlayers[i].transform);
                }
                else
                {
                    res += SaveInfoShip();
                }
            }
        }

        return res;
    }

    private string SaveRaycasts()
    {
        string hitResults = "";
        float raycastAngle = nbRaycasts > 1 ? -0.5f * angleShipFieldview : 0;
        Vector2 direction;
        for (int i = 0; i < nbRaycasts; i++)
        {
           
            direction = Trigonometry.RotateVector(transformShipPlayer.up, raycastAngle * Mathf.Deg2Rad);
            hitResults += RunRaycast(direction)+";";
            raycastAngle += angleDiffBetweenRaycast;
        }

        return hitResults;
    }

    private string RunRaycast(Vector2 direction)
    {
        Physics2D.Raycast(headPosition.position, direction, (new ContactFilter2D()).NoFilter(), result, Mathf.Infinity);

        RaycastHit2D hit = result[0];
        return hit.distance + ";" + hit.normal.x + ";" + hit.normal.y;
    }
}

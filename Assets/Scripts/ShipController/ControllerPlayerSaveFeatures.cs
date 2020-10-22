using System.Runtime.Versioning;
using UnityEngine;

public class ControllerPlayerSaveFeatures : ControllerPlayer
{
    private Player player;
    private ManagePlayers managePlayers;
    private Transform positionHead;

    public float frqceSaveDataHz = 5f;
    private float countBetweenCapture;
    private float countSinceLastCapture = 0f;

    private bool isSavingData;

    //RayCast
    private RaycastHit2D[] result;
    public int nbrMaxResultNByRayCast = 1; 
    public int nbrRayCasts = 50;
    public float angleCastingRayCast = 300f;
    private float differenceAngleBetweenRay;
    private float secondsNotSavedBeforeDeath = 3f;

    private void Start()
    {
        isSavingData = true;
        positionHead = GetComponent<Snake>().positionHotSpotFront;
        result = new RaycastHit2D[nbrMaxResultNByRayCast];
        float nbrDivision = nbrRayCasts > 1 ? nbrRayCasts - 1 : 1;
        differenceAngleBetweenRay = angleCastingRayCast/ nbrDivision;
        countBetweenCapture=  1/(frqceSaveDataHz*Time.fixedDeltaTime);
        player = GetComponent<Player>();
        managePlayers = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>();
    }

    //return -1 for left, 1 for right and 0 equals forward
    override public float GetRotation()
    {

        float input = Input.GetAxis(moveAxis);
        if (input != 0)
        {
            input = 1f*Mathf.Sign(input);
        }

        if (isSavingData)
        {
            if (managePlayers.nbrPlayerDead > 0)
            {
                isSavingData = false;
                float nbrLinesToDelete = secondsNotSavedBeforeDeath /( countBetweenCapture * Time.fixedDeltaTime);
                DataWriter.instance.DeleteLastLines((int)nbrLinesToDelete);
            }
            else if (countSinceLastCapture > countBetweenCapture)
            {
                SaveData(input);
                countSinceLastCapture = 0;
            }
        }

        countSinceLastCapture++;


        return input;
    }

    private void SaveData(float decision)
    {
        DataWriter.instance.writeInfoSpaceship(SaveInfoShip(transform), SaveRayCasts(),SaveOtherShipsInfo(),decision);
    }

    private string SaveOtherShipsInfo()
    {
        string res="";
        for(int i = 0;i < managePlayers.usableSpaceships.Length;i++)
        {
            if( i != player.id)
            {
                res += "[";
                if(i < managePlayers.activePlayers.Length)
                {
                    res += SaveInfoShip(managePlayers.activePlayers[i].transform);
                }
                else
                {
                    res += SaveInfoShip();
                }
                res += "]";
            }
        }

        return res;
    }

    private string SaveInfoShip(Transform spaceship = null)
    {
        if(spaceship == null)
        {
            return "0;0;0";
        }
        else
        {
            return spaceship.position.x + ";" + spaceship.position.y + ";" + spaceship.rotation.eulerAngles.z;
        }
    }

    private string SaveRayCasts()
    {
        string resultHits = "";
        float originAngle = nbrRayCasts > 1 ? -0.5f* angleCastingRayCast : 0;
        Vector2 direction;
        for (int i = 0; i< nbrRayCasts; i++)
        {
            direction = Rotate(transform.up, originAngle);
            resultHits += "["+originAngle +";"+RunRayCast(direction)+"]";
            originAngle += differenceAngleBetweenRay;
        }

        return resultHits;
    }

    private string RunRayCast(Vector2 direction)
    {
        Debug.DrawRay(positionHead.position, direction, Color.green, 1/frqceSaveDataHz);
        Physics2D.Raycast(positionHead.position, direction, (new ContactFilter2D()).NoFilter(), result, Mathf.Infinity);

        RaycastHit2D hit = result[0];
        return hit.distance + ";"+ hit.normal.x + ";" + hit.normal.y;
    }

    private Vector2 Rotate(Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}

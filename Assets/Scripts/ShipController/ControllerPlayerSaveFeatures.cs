using System.Runtime.Versioning;
using UnityEngine;

public class ControllerPlayerSaveFeatures : ControllerPlayer
{
    private Player player;
    private ManagePlayers managePlayers;

    public float frqceSaveDataHz = 5f;
    private float countBetweenCapture;
    private float countSinceLastCapture = 0f;

    //RayCast
    private RaycastHit2D[] result;
    public int nbrMaxResultNByRayCast = 1; 
    public int nbrRayCasts = 50;
    public float angleCastingRayCast = 300f;
    private float differenceAngleBetweenRay;

    private void Start()
    {
        result = new RaycastHit2D[nbrMaxResultNByRayCast];
        differenceAngleBetweenRay = (angleCastingRayCast*Mathf.PI/ 180 )/ nbrRayCasts;
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

        if (countSinceLastCapture > countBetweenCapture)
        {
            SaveData(input);
            countSinceLastCapture = 0;
        }

        countSinceLastCapture++;


        return input;
    }

    private void SaveData(float decision)
    {
        DataWriter.instance.writeInfoSpaceship(transform.position, transform.eulerAngles.z, SaveRayCasts(),decision);
    }

    private string SaveRayCasts()
    {
        string resultHits = "";
        float originAngle = -0.5f* (angleCastingRayCast * Mathf.PI / 180);
        Vector2 direction = transform.up;
        for (int i = 0; i< nbrRayCasts; i++)
        {
            resultHits += originAngle +";"+RunRayCast(direction);
            originAngle += differenceAngleBetweenRay;
            direction = Rotate(transform.up,originAngle);
        }

        return resultHits;
    }

    private string RunRayCast(Vector2 direction)
    {
        Debug.DrawRay(transform.position, direction, Color.green);
        Physics2D.Raycast(transform.position, direction, (new ContactFilter2D()).NoFilter(), result, Mathf.Infinity);

        RaycastHit2D hit = result[0];
        return "[" + hit.distance + ";"+ hit.normal.x + ";" + hit.normal.y+ "]";
    }

    Vector2 TranslateTransformUpByAngle(float radians)
    {
        float vectorAngle = Mathf.Atan(transform.up.y / transform.up.x) + radians;
        return new Vector2(
            Mathf.Cos(vectorAngle),
            Mathf.Sin(vectorAngle)
        );
    }

    private Vector2 Rotate(Vector2 v, float rad) {
        float degrees = rad * 180 / Mathf.PI;
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}

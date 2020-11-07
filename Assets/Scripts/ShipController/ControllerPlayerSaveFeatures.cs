using UnityEngine;

public class ControllerPlayerSaveFeatures : ControllerPlayer
{
    private Player player;
    private ManagePlayers managePlayers;
    private Transform headPosition;

    public float dataSavingFrequence = 5f; // in number of frame
    private float countBetweenCapture;
    private float countSinceLastCapture = 0f;

    private bool isSavingData;

    //RayCast
    private RaycastHit2D[] result;
    public int nbMaxResultsByRaycast = 1;
    public int nbRaycasts = 50;
    public float angleShipFieldview = 300f; // in degrees
    private float angleDiffBetweenRaycast; // in degrees
    private float timeNotSavedBeforeDeath = 3f; // in seconds

    private void Start()
    {
        isSavingData = true;
        headPosition = GetComponent<Snake>().positionHotSpotFront;
        result = new RaycastHit2D[nbMaxResultsByRaycast];
        float nbDivision = nbRaycasts > 1 ? nbRaycasts - 1 : 1;
        angleDiffBetweenRaycast = angleShipFieldview / nbDivision;
        countBetweenCapture = 1 / (dataSavingFrequence * Time.fixedDeltaTime);
        player = GetComponent<Player>();
        managePlayers = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>();
    }

    //return -1 for left, 1 for right and 0 equals forward
    override public float GetRotation()
    {
        float input = Input.GetAxis(moveAxis);
        if (input != 0)
        {
            input = Mathf.Sign(input);
        }

        if (isSavingData)
        {
            if (managePlayers.nbrPlayerDead > 0)
            {
                isSavingData = false;
                float nbLinesToDelete = timeNotSavedBeforeDeath / (countBetweenCapture * Time.fixedDeltaTime);
                DataWriter.instance.DeleteLastLines((int)nbLinesToDelete);
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
        DataWriter.instance.WriteInfoSpaceship(
            infoShip: SaveInfoShip(transform),
            raycasts: SaveRaycasts(),
            otherShipsInfo: SaveOtherShipsInfo(),
            decision: decision
        );
    }

    private string SaveInfoShip(Transform spaceship = null)
    {
        if (spaceship == null)
        {
            return "0;0;0";
        }
        else
        {
            return spaceship.position.x + ";" + spaceship.position.y + ";" + spaceship.rotation.eulerAngles.z;
        }
    }

    private string SaveOtherShipsInfo()
    {
        string res = "";
        for (int i = 0; i < managePlayers.usableSpaceships.Length; i++)
        {
            if (i != player.id)
            {
                res += ";";
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
            if(i != 0) 
            {
                hitResults += ";";
            }
            direction = Trigonometry.RotateVector(transform.up, raycastAngle * Mathf.Deg2Rad);
            hitResults += raycastAngle + ";" + RunRaycast(direction);
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

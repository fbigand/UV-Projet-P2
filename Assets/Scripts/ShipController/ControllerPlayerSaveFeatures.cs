using UnityEngine;

public class ControllerPlayerSaveFeatures : ControllerPlayer
{
    private ManagePlayers managePlayers;

    public float dataSavingFrequence = 5f; // in number of frame
    private float countBetweenCapture;
    private float countSinceLastCapture = 0f;

    private bool isSavingData;

    //RayCast
    public int nbMaxResultsByRaycast = 1;
    public int nbRaycasts = 50;
    public float angleShipFieldview = 300f; // in degrees
    private float timeNotSavedBeforeDeath = 3f; // in seconds

    private SaveDataLearning saveDataGame;

    private void Start()
    {
        isSavingData = true;
        countBetweenCapture = 1 / (dataSavingFrequence * Time.fixedDeltaTime);
        managePlayers = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>();
        saveDataGame = new SaveDataLearning(transform,managePlayers,angleShipFieldview,nbRaycasts, GetComponent<Snake>().positionHotSpotFront, GetComponent<Player>());
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
                DataWriter.instance.Write(saveDataGame.Save()+input);
                countSinceLastCapture = 0;
            }
        }
        countSinceLastCapture++;

        return input;
    }

    

   

   
   
   
}

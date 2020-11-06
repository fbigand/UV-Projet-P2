using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagePlayers : MonoBehaviour
{
    public GameObject[] usableSpaceships;

    private int numberPlayers;
    public int countdownTime; // in seconds
    public Text countdownText;
    public string startMessage;
    public int startMessageDuration; // in seconds
    private bool isGameRunning;

    [HideInInspector]
    public GameObject[] activePlayers;

    //Manage scores
    public int nbrPlayerDead = 0;
    private int nbrPointByRank = 10;
    public GameObject HUD;
    public HudPlayer hudPlayerPrefab;

    //Show scores
    public GameObject roundResults;
    public HudScore hudScorePrefab;



    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        numberPlayers = GameSettings.instance.GetNumberPlayers();
        activePlayers = new GameObject[numberPlayers];
        if (usableSpaceships != null && usableSpaceships.Length > 0)
        {
            for (int i = 0; i < usableSpaceships.Length; i++)
            {
                if (i < numberPlayers)
                {
                    activePlayers[i] = usableSpaceships[i];
                }
                else
                {
                    Destroy(usableSpaceships[i].gameObject);
                }
            }
            GiveControllerToShips();
            PlacePlayers();
            StartCoroutine(Countdown());
            AssociateHud();
        }
    }

    private void GiveControllerToShips()
    {
        for (int i = 0; i < GameSettings.instance.indexController.Count; i++)
        {
            switch (GameSettings.instance.indexController[i])
            {
                case 0: // Player
                    ControllerPlayerSaveFeatures player = usableSpaceships[i].AddComponent<ControllerPlayerSaveFeatures>();
                    player.moveAxis = "Move" + (i + 1).ToString();
                    player.attackAxis = "Power" + (i + 1).ToString();
                    break;
                case 1: // AI Easy
                    usableSpaceships[i].AddComponent<ControllerIALearning>();
                    break;
                case 2: // AI Medium
                    ControllerIAMedium medium = usableSpaceships[i].AddComponent<ControllerIAMedium>();
                    medium.raycastNumber = 9;
                    break;
                case 3: // AI Hard
                    ControllerIAMediumHard hard = usableSpaceships[i].AddComponent<ControllerIAMediumHard>();
                    hard.distanceCaptureRay = 100;
                    hard.raycastNumber = 700;
                    break;
                case 4: // Save Data Player
                    ControllerPlayerSaveFeatures playerSaveFeatures = usableSpaceships[i].AddComponent<ControllerPlayerSaveFeatures>();
                    playerSaveFeatures.angleShipFieldview = 200;
                    playerSaveFeatures.dataSavingFrequence = 5;
                    playerSaveFeatures.nbRaycasts = 50;
                    playerSaveFeatures.nbMaxResultsByRaycast = 1;
                    playerSaveFeatures.moveAxis = "Move" + (i + 1).ToString();
                    playerSaveFeatures.attackAxis = "Power" + (i + 1).ToString();
                    break;
                default: // Consider AI Easy
                    usableSpaceships[i].AddComponent<ControllerRandom>();
                    break;
            }
        }
    }

    private void PlacePlayers()
    {
        float rayon = 4;
        Vector2 center = new Vector2(4, 0);
        float incrArc = 2 * Mathf.PI * rayon / numberPlayers;
        float longueurArc = 0;

        float angle;
        float posX;
        float posY;

        for (int i = 0; i < activePlayers.Length; i++)
        {

            angle = longueurArc / rayon;
            posX = rayon * Mathf.Cos(angle) + center.x;
            posY = rayon * Mathf.Sin(angle) + center.y;


            activePlayers[i].transform.position = new Vector3(posX, posY, -0.1f);
            OrientateSpaceship(activePlayers[i].transform, center);

            longueurArc += incrArc;
        }
    }

    private void OrientateSpaceship(Transform spaceship, Vector2 lookPoint)
    {
        Vector3 diff = lookPoint - new Vector2(spaceship.position.x, spaceship.position.y);
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        spaceship.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        countdownText.text = startMessage;
        yield return new WaitForSeconds(startMessageDuration);
        countdownText.gameObject.SetActive(false);
        StartGame();

        yield return null;
    }

    private void AssociateHud()
    {
        int posy = 350;
        int incrPosY = -200;
        foreach (GameObject spaceship in activePlayers)
        {
            HudPlayer hudPlayer = Instantiate(hudPlayerPrefab) as HudPlayer;

            hudPlayer.transform.SetParent(HUD.transform);
            hudPlayer.transform.localScale = new Vector3(1, 1, 1);
            hudPlayer.transform.localPosition = new Vector3(-700, posy, -1f);

            spaceship.GetComponent<Player>().hudplayer = hudPlayer;
            posy += incrPosY;

        }
    }

    private void ShowScore()
    {
        Player[] rankedPLayers = GetPlayersRankOrdered();
        int posx = -373;
        int posy = 120;
        int incrPosY = -90;

        for (int i = 0; i < rankedPLayers.Length; i++)
        {
            HudScore hudScore = Instantiate(hudScorePrefab) as HudScore;
            hudScore.transform.SetParent(roundResults.transform);
            hudScore.transform.localScale = new Vector3(1, 1, 1);
            hudScore.transform.localPosition = new Vector3(posx, posy, -1f);
            hudScore.SetPlayer(rankedPLayers[i], i + 1);
            posy += incrPosY;
        }
    }

    private Player[] GetPlayersRankOrdered()
    {
        Player[] players = new Player[activePlayers.Length];
        for (int i = 0; i < activePlayers.Length; i++)
        {
            players[i] = activePlayers[i].GetComponent<Player>();
        }

        Array.Sort(players, (player1, player2) => player2.score - player1.score);

        return players;
    }

    private void StartGame()
    {
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (i >= Scores.scores.Count)
            {
                Scores.scores.Add(0);
            }

            activePlayers[i].SetActive(true);
            activePlayers[i].gameObject.GetComponent<Player>().Init(
                id: i,
                score: Scores.scores[i],
                pseudo: GameSettings.instance.playerPseudos[i]
            );
        }

    }


    public void PlayerFinishGame(Player playerDead)
    {
        if (!isGameRunning)
        {
            return;
        }

        SaveScoreToPlayer(playerDead);
        nbrPlayerDead++;

        if (nbrPlayerDead == numberPlayers - 1)
        {
            for (int i = 0; i < activePlayers.Length; i++)
            {
                Player player = activePlayers[i].gameObject.GetComponent<Player>();
                if (player.isAlive)
                {
                    SaveScoreToPlayer(player);
                    StartCoroutine(LoadNextRound());
                }
            }
        }
        else if (nbrPlayerDead == numberPlayers)
        {
            StartCoroutine(LoadNextRound());
        }
    }

    private void SaveScoreToPlayer(Player player)
    {
        int gain = nbrPointByRank * nbrPlayerDead;
        Scores.scores[player.id] += gain;
        player.score = Scores.scores[player.id];
        player.gain = gain;
    }

    private IEnumerator LoadNextRound()
    {
        isGameRunning = false;
        roundResults.SetActive(true);
        ShowScore();
        yield return new WaitForSeconds(5);
        roundResults.SetActive(false);
        SceneManager.LoadScene(1);
    }
}

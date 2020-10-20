using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagePlayers : MonoBehaviour
{
    public GameObject[] usableSpaceships;

    public int numberPlayers = 1;
    public int countdownTime; // in seconds
    public Text countdownText;
    public string startMessage;
    public int startMessageDuration; // in seconds

    public GameObject[] activePlayers;

    //Manage scores
    private int nbrPlayerDead = 0;
    private int nbrPointByRank = 10;
    public GameObject HUD;
    public HudPlayer hudPlayerPrefab;

    //Show scores
    public GameObject roundResults;
    public HudScore hudScorePrefab;

    // Start is called before the first frame update
    void Start()
    {
        activePlayers = new GameObject[numberPlayers];

        if(usableSpaceships != null && usableSpaceships.Length > 0)
        {
            for(int i = 0; i< usableSpaceships.Length; i++)
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
            placePlayers();
            StartCoroutine(Countdown());
            associateHud();
        }
    }

    private void Update()
    {

    }

    private void placePlayers()
    {
        float rayon = 4;
        Vector2 center= new Vector2(4, 0);
        float incrArc = 2 * Mathf.PI * rayon / numberPlayers;
        float longueurArc = 0;

        float angle;
        float posX;
        float posY;

        for (int i = 0; i < activePlayers.Length; i++)
        {
            
            angle = longueurArc  /  rayon;
            posX = rayon * Mathf.Cos(angle) +center.x;
            posY = rayon * Mathf.Sin(angle) + center.y;

          
            activePlayers[i].transform.position = new Vector3(posX, posY, -0.1f);
            orientateSpaceship(activePlayers[i].transform, center);

            longueurArc += incrArc;
        }
    }

    private void orientateSpaceship(Transform spaceship, Vector2 lookPoint)
    {
        Vector3 diff = lookPoint - new Vector2(spaceship.position.x,spaceship.position.y);
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

    private void associateHud()
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

    private void showScore()
    {
        int posy = 65;
        int incrPosY = -40;

        foreach (GameObject spaceship in activePlayers)
        {
            HudScore hudScore = Instantiate(hudScorePrefab) as HudScore;
            hudScore.transform.SetParent(roundResults.transform);
            hudScore.transform.localScale = new Vector3(1, 1, 1);
            hudScore.transform.localPosition = new Vector3(0, posy, -1f);
            posy += incrPosY;
        }


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
            activePlayers[i].gameObject.GetComponent<Player>().init(i, Scores.scores[i]);
        }

    }


    public int playerFinishGame(int id)
    {
        Scores.scores[id] += nbrPointByRank * nbrPlayerDead;
        nbrPlayerDead++;

        if(nbrPlayerDead == numberPlayers - 1)
        {
            for (int i = 0; i < activePlayers.Length; i++)
            {
                Player player = activePlayers[i].gameObject.GetComponent<Player>();
                if (player.isAlive )
                {
                    Scores.scores[player.id] += nbrPointByRank * nbrPlayerDead;
                    StartCoroutine(loadNextRound());
                }
            }            
        }else if(nbrPlayerDead == numberPlayers)
        {
            StartCoroutine(loadNextRound());
        }
        return Scores.scores[id];
    }

    private IEnumerator loadNextRound()
    {
        roundResults.SetActive(true);
 //       showScore();

        yield return new WaitForSeconds(5);
        roundResults.SetActive(false);
        SceneManager.LoadScene(1);
    }
}

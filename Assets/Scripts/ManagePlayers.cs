using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ManagePlayers : MonoBehaviour
{
    public GameObject[] usableSpaceships;

    public int numberPlayers = 1;
    public int countdownTime; // in seconds
    public Text countdownText;
    public string startMessage;
    public int startMessageDuration; // in seconds

    private GameObject[] activePlayers;

    //Manage scores
    private static int[] scores;
    private int nbrPlayerDead = 0;
    private int nbrPointByRank = 10;
    public GameObject HUD;
    public HudPlayer hudPlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        scores = new int[numberPlayers];
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
            associateHud();
            StartCoroutine(Countdown());
        }
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
        int posy = 279;
        int incrPosY = -200;
        foreach (GameObject spaceship in activePlayers)
        {
            HudPlayer hudPlayer = Instantiate(hudPlayerPrefab) as HudPlayer;

            hudPlayer.transform.SetParent(HUD.transform);
            hudPlayer.transform.localScale = new Vector3(1, 1, 1);
            hudPlayer.transform.localPosition = new Vector3(-1130, posy, -1f);
            
            
            spaceship.GetComponent<Player>().hudplayer = hudPlayer;
            posy += incrPosY;
            //hudPlayer.transform.Translate(new Vector3(0f, -100f, 0f));
        }
    }

    private void StartGame()
    {
        for (int i = 0; i < activePlayers.Length; i++)
        {
            activePlayers[i].SetActive(true);
            activePlayers[i].gameObject.GetComponent<Player>().init(i, scores[i]);
        }
    }

    public int playerFinishGame(int id)
    {
        scores[id] += nbrPointByRank * nbrPlayerDead;
        nbrPlayerDead++;
        return scores[id];
    }
}

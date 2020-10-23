using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int score;
    [HideInInspector]
    public HudPlayer hudplayer;
    public Color color;
    public bool isAlive;

    public void init(int id, int score)
    {
        this.score = score;
        this.id = id;
        this.isAlive = true;
        hudplayer.SetPlayer("Player:" + id, color, score.ToString());
    }

    public void finishRound()
    {
        isAlive = false;
        score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().playerFinishGame(id);
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    internal int id;
    internal int score;
    internal int gain;
    internal HudPlayer hudplayer;
    public Color color;
    internal bool isAlive;
    internal string pseudo;

    public void Init(int id, int score, string pseudo=null)
    {
        this.score = score;
        this.id = id;
        this.pseudo = pseudo ?? "Player" + (id + 1).ToString();
        this.isAlive = true;
        hudplayer.SetPlayer(this.pseudo, color,score.ToString());
    }

    public void FinishRound()
    {
        isAlive = false;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().PlayerFinishGame(this);
    }

}

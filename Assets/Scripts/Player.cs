using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int score;
    [HideInInspector]
    public HudPlayer hudplayer;
    public HudScore hudscore;
    public Color color;
    public bool isAlive;
    public string namePlayer;

    public void init(int id, int score)
    {
        this.score = score;
        this.id = id;
        this.namePlayer = "Player" + (id + 1).ToString();
        this.isAlive = true;
        hudplayer.SetPlayer(namePlayer, color,score.ToString());
 //       hudscore.SetPlayer(namePlayer, color, score.ToString());
    }

    public void finishRound()
    {
        isAlive = false;
        score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().playerFinishGame(id);
    }
}

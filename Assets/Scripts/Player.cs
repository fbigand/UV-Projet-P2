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
    public Color color;

    public void init(int id, int score)
    {
        this.score = score;
        this.id = id;
        hudplayer.SetPlayer("Player:" + id, color);
    }

    public void finishRound()
    {
        score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().playerFinishGame(id);
    }
}

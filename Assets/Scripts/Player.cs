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

    public void init(int id, int score)
    {
        this.score = score;
        this.id = id;
    }

    public void finishRound()
    {
        score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().playerFinishGame(id);
    }
}

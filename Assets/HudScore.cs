using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScore : MonoBehaviour
{
    private Text gainLevel;
    private Text rankText;
    private Text playerName;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rankText = transform.Find("RankText").GetComponent<Text>();
        gainLevel = transform.Find("GainLevel").GetComponent<Text>();
        playerName = transform.Find("PlayerName").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    public void SetPlayer(string name, Color color, string score)
    {
        playerName.text = name;
        playerName.color = color;
        scoreText.text = score;
    }

    public void SetTextScore(string score, string gain)
    {
        gainLevel.text = gain;
        scoreText.text = score;
    }

    public void SetRank(string rank)
    {
        rankText.text = rank;
    }

}

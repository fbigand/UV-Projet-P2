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
    void Awake()
    {
        rankText = transform.Find("RankText").GetComponent<Text>();
        gainLevel = transform.Find("GainLevel").GetComponent<Text>();
        playerName = transform.Find("PlayerName").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    public void SetPlayer(Player player)
    {
        playerName.text = player.pseudo;
        playerName.color = player.color;
        scoreText.text = player.score.ToString();
    }
}

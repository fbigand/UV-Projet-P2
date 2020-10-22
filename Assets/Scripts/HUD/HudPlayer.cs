
using UnityEngine;
using UnityEngine.UI;

public class HudPlayer : MonoBehaviour
{
    private Text textBonusPrimary;
    private Text textBonusSecondary;
    private Text playerName;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        textBonusSecondary = transform.Find("DownBonusText").gameObject.GetComponent<Text>();
        textBonusPrimary = transform.Find("UpBonusText").GetComponent<Text>();
        playerName = transform.Find("PlayerName").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent <Text>();
    }

    public void SetPlayer(string name, Color color, string score)
    {
        playerName.text = name;
        playerName.color = color;
        scoreText.text = score;
    }

    public void SetTextBonus(string countDownPrimaryBonus, string countDownSecondaryBonus)
    {
        textBonusPrimary.text = countDownPrimaryBonus;
        textBonusSecondary.text = countDownSecondaryBonus;
    }

    public void SetPrimaryToReady()
    {
        textBonusPrimary.text = "Ready";
    }
    public void SetSecondaryToReady()
    {
        textBonusSecondary.text = "Ready";
    }

    public void setScoreText(string score)
    {
        scoreText.text = score;
    }

}

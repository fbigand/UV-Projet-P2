
using UnityEngine;
using UnityEngine.UI;

public class HudPlayer : MonoBehaviour
{
    private Text textCooldownUp;
    private Text textCooldownDown;
    private Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        textCooldownDown = transform.Find("DownBonusText").gameObject.GetComponent<Text>();
        textCooldownUp = transform.Find("UpBonusText").GetComponent<Text>();
        playerName = transform.Find("PlayerName").GetComponent<Text>();
    }

    public void setNamePLayer()
    {

    }

    public void setTextBonusUp()
    {

    }

    public void setBonusDown()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

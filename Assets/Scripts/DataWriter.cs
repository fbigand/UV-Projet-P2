using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    private string fileName = "data.csv";
    private StreamWriter sw;

    public static DataWriter instance = null;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (sw != null)
        {
            sw.Flush();
        }
       sw = new StreamWriter(Application.dataPath + "/Data/" + fileName);
    }

    private void write(string text)
    {
        sw.Write(text);
        sw.Flush();
    }

    public void writePos(Vector2 pos)
    {
        write(pos.x + ";" + pos.y + ";");
    }

    public void writeDecisionPlayer(float decision)
    {
       write(decision.ToString()+"\n");
    }
}

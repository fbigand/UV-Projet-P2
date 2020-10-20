using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    Queue<string> listToWrite;
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
        listToWrite = new Queue<string>();
        sw = new StreamWriter(Application.dataPath + "/Data/" + fileName);


    }

    public void Update()
    {
        if (listToWrite.Count > 0)
        {
            sw.Write(listToWrite.Dequeue());
            sw.Flush();
        }

    }


    public void writePos(Vector2 pos)
    {
        listToWrite.Enqueue(pos.x + "," + pos.y+",");
    }
}

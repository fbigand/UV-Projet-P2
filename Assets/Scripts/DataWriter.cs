using System.IO;
using System.Linq;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    private string fileName = "data.csv";
    private string fileNameBeforeDeath = "data_before_death.csv";
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

    public void writeInfoSpaceship(string infoShip, string rayCasts, string otherShipsInfo, float decision)
    {
        write(infoShip + ";[" + rayCasts + "];[" + otherShipsInfo + "];" + decision + "\n");
    }

    public void DeleteLastLines(int nbrLineToDelete)
    {
        sw.Flush();
        sw.Dispose();
        sw.Close();
        int nbrLineToKeep = File.ReadLines(Application.dataPath + "/Data/" + fileName).Count() - nbrLineToDelete;
        string line;
        int nbrCurrentLine = 0;
        using (StreamReader reader = File.OpenText(Application.dataPath + "/Data/" + fileName))
        {
            using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Data/" + fileNameBeforeDeath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    nbrCurrentLine++;

                    if (nbrCurrentLine >= nbrLineToKeep)
                    {
                        break;
                    }

                    writer.WriteLine(line);
                }
            }
        }
    }
}

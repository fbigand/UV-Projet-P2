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

    private void Write(string text)
    {
        sw.Write(text);
        sw.Flush();
    }

    public void WriteInfoSpaceship(string infoShip, string raycasts, string otherShipsInfo, float decision)
    {
        Write(infoShip + ";[" + raycasts + "];[" + otherShipsInfo + "];" + decision + "\n");
    }

    public void DeleteLastLines(int nbLineToDelete)
    {
        sw.Flush();
        sw.Dispose();
        sw.Close();
        int nbLineToKeep = File.ReadLines(Application.dataPath + "/Data/" + fileName).Count() - nbLineToDelete;
        string line;
        int currentLine = 0;
        using (StreamReader reader = File.OpenText(Application.dataPath + "/Data/" + fileName))
        {
            using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Data/" + fileNameBeforeDeath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    currentLine++;

                    if (currentLine >= nbLineToKeep)
                    {
                        break;
                    }

                    writer.WriteLine(line);
                }
            }
        }
    }
}

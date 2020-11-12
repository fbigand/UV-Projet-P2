using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    private string fileNameAllData = "data.csv";
    List<string> SavedData = new List<string>();

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

    public void Write(string text)
    {
        SavedData.Add(text);
    }

    public void WriteInfoSpaceship(string infoShip, string raycasts, string otherShipsInfo, float decision)
    {
        Write(infoShip + ";" + raycasts + otherShipsInfo + ";" + decision);
    }

    public void DeleteLastLines(int nbLineToDelete)
    {
       
        string line;

        if (SavedData.Count > nbLineToDelete)
        {
            SavedData.RemoveRange(SavedData.Count - nbLineToDelete, nbLineToDelete);
        }
        else
        {
            SavedData.Clear();
        }


        using (StreamReader reader = File.OpenText(Application.dataPath + "/Data/" + fileNameAllData))
        {
            while ((line = reader.ReadLine()) != null)
            {
                SavedData.Add(line);
            }
        }

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Data/" + fileNameAllData))
        {
            foreach(string lineSaved in SavedData)
            {
                writer.WriteLine(lineSaved);
            }
        }

        SavedData.Clear();

    }
}

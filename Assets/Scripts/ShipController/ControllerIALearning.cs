﻿
using System.Diagnostics;
using System.IO;

using UnityEngine;

public class ControllerIALearning : ControllerIA
{
    // Start is called before the first frame update

    private ManagePlayers managePlayers;
    public string pathPythonExe = "C:\\Users\\emile\\Documents\\télécom\\Computer Vision\\TP-1dollar-pyqt\\python-3.8.5.exe";
    public float dataSavingFrequence = 50f; 


    //RayCast
    public int nbMaxResultsByRaycast = 1;
    public int nbRaycasts = 50;
    public float angleShipFieldview = 300f; // in degrees

    private ReaderDataLearning dataGameReader;

    private void Start()
    {
        managePlayers = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>();
        dataGameReader = new ReaderDataLearning(transform, managePlayers, angleShipFieldview, nbRaycasts, GetComponent<Snake>().positionHotSpotFront, GetComponent<Player>());
    }

    public override float GetRotation()
    {
        string res = runPredict(dataGameReader.Read());
        return int.Parse(res);
    }

    public override bool IsUsingPrimaryBonus()
    {
        return false;
    }

    public override bool IsUsingSecondaryBonus()
    {
        return false;
    }

    public string runPredict(string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = pathPythonExe;
        start.Arguments = string.Format("\"{0}\" \"{1}\"", Application.dataPath + "/Data/" + "predict.py", args);
        start.UseShellExecute = false;// Do not use OS shell
        start.CreateNoWindow = true; // We don't need new window
        start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
        start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                return result;
            }
        }
    }
}
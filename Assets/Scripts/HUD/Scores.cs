using System.Collections.Generic;
using UnityEngine;

public class Scores : ScriptableObject
{
    public static List<int> scores = new List<int>();

    public static void reset()
    {
        scores.Clear();
    }

}

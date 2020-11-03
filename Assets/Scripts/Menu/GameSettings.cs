using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool pickUp = true;
    public List<int> indexController = new List<int>();
    public List<string> playerPseudos = new List<string>();

    public static GameSettings instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    internal int GetNumberPlayers()
    {
        return playerPseudos.Count;
    }

    internal void Clear()
    {
        indexController.Clear();
        playerPseudos.Clear();
    }
}

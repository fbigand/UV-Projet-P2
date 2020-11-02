using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool pickUp = true;
    public List<int> indexController = new List<int>();
    public List<string> playerPseudos = new List<string>();
    internal int nbPlayers;

    public static GameSettings instance;

    private void Start()
    {
        nbPlayers = playerPseudos.Count;
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
}

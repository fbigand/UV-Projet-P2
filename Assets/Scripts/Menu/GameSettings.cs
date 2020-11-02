using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public int nbPlayers = 0;
    public bool pickUp = true;
    public List<int> indexController = new List<int>();
    public List<string> PlayerName = new List<string>();

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
}

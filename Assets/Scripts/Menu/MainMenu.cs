using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<Dropdown> dropdown;
    public List<InputField> inputfield;

    public void PlayGame()
    {
        ReadDropDown();
        ReadInputField();
        print("index :" + GameSettings.instance.indexController);
        print("playerName :" + GameSettings.instance.PlayerName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void ReadDropDown()
    {
        for (int i = 0; i < dropdown.Count; i++)
            {
                GameSettings.instance.indexController.Add(dropdown[i].value);
            }
    }

    public void ReadInputField()
    {
        for (int i = 0; i < inputfield.Count; i++)
        {
            GameSettings.instance.PlayerName.Add(inputfield[i].text);
        }
    }
}



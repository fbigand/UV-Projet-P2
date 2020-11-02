using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    private Dropdown dropdown;
    private InputField inputfield;

    void Awake()
    {
        dropdown = gameObject.GetComponentInChildren<Dropdown>();
        inputfield = gameObject.GetComponentInChildren<InputField>();
    }

    public void Toggle_Changed(bool value)
    {
        dropdown.interactable = value;
        inputfield.interactable = value;

        if (value == true)
        {
            GameSettings.instance.nbPlayers += 1;
        }
        else
        {
            GameSettings.instance.nbPlayers -= 1;
        }
    }

    public void Toggle_PickUps(bool value)
    {
        GameSettings.instance.pickUp = value;
        print(GameSettings.instance.pickUp);
    }
}

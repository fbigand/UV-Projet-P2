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

    public void ToggleChanged(bool value)
    {
        dropdown.interactable = value;
        inputfield.interactable = value;
    }

    public void TogglePickups(bool value)
    {
        GameSettings.instance.pickUp = value;
    }
}

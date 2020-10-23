using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("Player");
        items.Add("AI Easy");
        items.Add("AI Medium");
        items.Add("AI Hard");

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown);});
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        int ddindex = dropdown.value;
        GameSettings.instance.index.Add(ddindex);

    }
}

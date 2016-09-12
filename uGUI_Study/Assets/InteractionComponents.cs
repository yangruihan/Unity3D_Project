using UnityEngine;
using System.Collections;

public class InteractionComponents : MonoBehaviour {

    public void OnToggleValueChanged(bool value)
    {
        Debug.Log(value);
    }

    public void OnDropdownValueChange(int id)
    {
        Debug.Log(id);
    }

    public void OnButtonClick()
    {
        Debug.Log("<color=green>Hello</color>");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour {

    public string playerName;
    public Toggle debugToggle;

    public void OnEnter()
    {
        playerName = GetComponent<InputField>().text;
    }

    public void ToggleDebug()
    {
        PlayerTurn.isPlayerTurn = debugToggle.isOn;
        PlayerTurn.isDebugging = debugToggle.isOn;
    }
}

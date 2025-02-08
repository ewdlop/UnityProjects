using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject winText;
    public GameObject loseText;
    public GameObject drawText;
    public static GameObject WinText;
    public static GameObject LoseText;
    public static GameObject DrawText;
    void Start () {
        winText.SetActive(false);
        loseText.SetActive(false);
        drawText.SetActive(false);
        WinText = winText;
        LoseText = loseText;
        DrawText = drawText;
    }
}

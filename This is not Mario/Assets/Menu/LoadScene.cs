using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    public GameObject Stage1button;
    public GameObject Stage2button;
    public GameObject HtPPanel;
    public GameObject NormalMode;
    public GameObject ExtraButton;
    public GameObject ExtraMenu;
    public GameObject Credit;


    private void Start()
    {
        print(Application.persistentDataPath);
        GameControl.instance.Load();
        if (GameControl.BeatStage0)
        {

            Stage1button.SetActive(true);

        }
        if (GameControl.BeatStage1)
        {

            Stage2button.SetActive(true);

        }
        if (GameControl.BeatStage2)
        {

            ExtraButton.SetActive(true);

        }
    }

    public void LoadScenes(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        TouchingFlagPole.triggered = false;
        Launch.triggered = false;
        Debug.Log("clicked");
    }
    public void ClickExit()

    {
        Application.Quit();
    }
	
    public void CloseHowtoPlay()
    {
        HtPPanel.SetActive(false);
    }

    public void OpenHowtoPlay()
    {
        HtPPanel.SetActive(true);
    }
    public void OpenNormalMode()
    {
        NormalMode.SetActive(true);
    }
    public void CloseNormalMode()
    {
        NormalMode.SetActive(false);
    }


    public void OpenCredit()
    {
        Credit.SetActive(true);
    }
    public void CloseCredit()
    {
        Credit.SetActive(false);
    }

    public void OpenExtraMenu()
    {
        ExtraMenu.SetActive(true);
    }
    public void CloseExtraMenu()
    {
        ExtraMenu.SetActive(false);
    }
}

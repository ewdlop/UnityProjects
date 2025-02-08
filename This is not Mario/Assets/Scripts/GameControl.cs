using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {

    
    public GameObject gameoverText;
    public GameObject Loading;
    public GameObject Blackscreen;
    public GameObject Mario;
    public Text deathcountText;
    public bool gameover = false;
    

    public static GameControl instance;
    public static int deathcount;
    public static int coincount=0;
    public static int highscore;
    public static float highdistance;
    public static int saveindex=0;
    public static int stage;
    public static float distance=0;
    public static bool BeatStage0 = false;
    public static bool BeatStage1 = false;
    public static bool BeatStage2 = false;
    
    public static float[,] savepointx= new float[5, 5];
    public static float[,] savepointy= new float[5, 5];
    public static bool[,] reachsavepoint = new bool[5, 5];





    //Dontdestoryonload....not working as I expected...

    public GameObject Player;
    public GameObject Gamecontrol;
    public GameObject UI;
    public GameObject Camera;
    public GameObject Zones;
    


    void Awake () {


        Resetbool();
        if (SceneManager.GetActiveScene().buildIndex != 0 && stage != 3)
        {
            //1st level= Stage0

            reachsavepoint[0, 0] = true;
            reachsavepoint[1, 0] = false;
            reachsavepoint[0, 1] = false;
            reachsavepoint[1, 1] = false;
            reachsavepoint[2, 1] = false;
            reachsavepoint[0, 2] = false;
            reachsavepoint[1, 2] = false;
            reachsavepoint[2, 2] = false;
            reachsavepoint[3, 2] = false;
            reachsavepoint[4, 2] = false;
            savepointx[0, 0] = -4;
            savepointy[0, 0] = 3.8f;
            savepointx[1, 0] = 100.1f;
            savepointy[1, 0] = 3.8f;
            //Stage1
            savepointx[0, 1] = -0.4f;
            savepointy[0, 1] = 3.8f;
            savepointx[1, 1] = 23f;
            savepointy[1, 1] = 3.8f;
            savepointx[2, 1] = 113.66f;
            savepointy[2, 1] = 3.8f;
            //Stage2
            savepointx[0, 2] = -13.46f;
            savepointy[0, 2] = 41f;
            savepointx[1, 2] = 49.5f;
            savepointy[1, 2] = 10.9f;
            savepointx[2, 2] = 106.12f;
            savepointy[2, 2] = 10.9f;
            savepointx[3, 2] = 248.6f;
            savepointy[3, 2] = 17.3f;
            savepointx[4, 2] = 424.16f;
            savepointy[4, 2] = 19f;

            //4=Heavenmode
            savepointx[0, 4]= 5.808001f;
            savepointy[0, 4]= 1.36f;
        }
    

        //singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance== !this)
        {
            Destroy(gameObject);
        }
	}
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && stage != 3)
        {
            Debug.Log(saveindex + "a");
            Debug.Log(stage + "a");
            Mario.transform.position = new Vector3(savepointx[saveindex, stage], savepointy[saveindex, stage], -1);
            Mario.transform.position = new Vector3(savepointx[saveindex, stage], savepointy[saveindex, stage], -1);
        }
    }


    void Update () {
        if (stage == 3 && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z)))

        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            stage = 0;
        }

         if (gameover == true && (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Z)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (coincount > highscore)
                highscore = coincount;

            coincount = 0;
            distance = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            saveindex = 0;
            if (coincount > highscore)
                highscore = coincount;

            coincount = 0;
            distance = 0;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            
        
        }
    }

    //For doing something after a scene is loaded.
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        //Not going to remove these green texts,since they looke op.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);

        if(scene.buildIndex==1)
        {
            stage = 0;
            Debug.Log(mode);
        }
        if (SceneManager.GetActiveScene().buildIndex != 0) { 
            Loading.SetActive(false);
        Blackscreen.SetActive(false);
        }
    }



    public void MarioDied()
    {
        gameoverText.SetActive(true);
        gameover = true;
        deathcount += 1;
        GameObject control = GameObject.Find("Control");
        if (control.GetComponent<Control>().heavenmode)
        {
          
            if (coincount > highdistance)
                highdistance =coincount;

        }
        else {
            deathcountText.text = "Death: " + deathcount.ToString();
            if (coincount > highscore)
                highscore = coincount;
        }
        

        distance = 0;
        coincount = 0;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Progression.dat");
        PlayerData data = new PlayerData();
        data.BeatStage0 = BeatStage0;
        data.BeatStage1 = BeatStage1;
        data.BeatStage2 = BeatStage2;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/Progression.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Progression.dat", FileMode.Open);
            PlayerData data=(PlayerData)bf.Deserialize(file);
            file.Close();
            BeatStage0 = data.BeatStage0;
            BeatStage1 = data.BeatStage1;
            BeatStage2 = data.BeatStage2;

        }

    } 
    
    public void Resetbool()
    {
        Launch.triggered = false;
        Launch.flyingoff = false;
        TouchingFlagPole.touched = false;
        TouchingFlagPole.triggered = false;
        Johncenamove.eaten = false;
        CharacterControllers.isDead = false;
        CharacterControllers.starmode = false;
        Stuckonelevator.stuck = false;
        FlagpoleMove.rotate = false;
    }

    [Serializable]
    class PlayerData
    {
        public bool BeatStage0;
        public bool BeatStage1;
        public bool BeatStage2;
    }


}
 
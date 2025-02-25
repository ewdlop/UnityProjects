using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static int deathCount;
    public static float score;
    [SerializeField]
    public static float highScore;
    public Transform minSpawnBound;
    public Transform maxSpawnBound;

    public float minHazardCooldown = 2.0f;
    public float maxHazardCooldown = 8.0f;

    public GameObject[] hazards;
    public GameObject GameOverPanel;
    public Text GameOverMessageText;
    public string gameOverMessage;
    protected float timer;
    public string SaveFileName;

    public static bool isGameOver;

    // Use this for initialization
    protected virtual void Start()
    {
        timer = UnityEngine.Random.Range(minHazardCooldown, maxHazardCooldown);
        Load(SaveFileName);
        score = 0;
        isGameOver = false;
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime; // Score is based on how long player can keep VIP alive
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnHazard();
            }
        }
        else
        {
            GameOver();
        }
    }

    protected virtual void SpawnHazard()
    {
        float y = UnityEngine.Random.Range(
                minSpawnBound.position.y, maxSpawnBound.position.y);
        Vector3 spawnPos = new Vector3(
            UnityEngine.Random.Range(
                minSpawnBound.position.x, maxSpawnBound.position.x),
             y, y);

        // Spawn the object in a random position
        // within the box designated by the minSpawn and maxSpawn
        Instantiate(hazards[UnityEngine.Random.Range(0, hazards.Length)],
                    spawnPos, Quaternion.identity);
        // Decrement the spawn timer, so that the level gets more difficult
        Mathf.Clamp(maxHazardCooldown -= 0.1f, minHazardCooldown, maxHazardCooldown);
        // Reset spawn timer
        timer = UnityEngine.Random.Range(minHazardCooldown, maxHazardCooldown);
    }

    protected virtual void GameOver()
    {
        GameOverPanel.SetActive(true);

        TimeSpan timeSpan = TimeSpan.FromSeconds(GameController.score);
        GameOverMessageText.text = $"{gameOverMessage}\n<color=#ff0000>{timeSpan.Minutes:D2}</color> min <color=#ff0000>{timeSpan.Seconds:D2}</color> sec <color=#ff0000>{timeSpan.Milliseconds:D2}</color> ms";

        if (score >= highScore)
        {
            highScore = score;
            Save(highScore, SaveFileName);
        }


    }
    public static void Save(float highScore, string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        //remeber to change the file name during each Ludum Dare
        FileStream saveFile =
            File.Create($"{Application.persistentDataPath}/{name}.dat");
        PlayerData data = new PlayerData(highScore);
        data.highScore = highScore;
        bf.Serialize(saveFile, data);
        saveFile.Close();

        //UI.cs updates highscore to score, and the HUD
        //this if statement must uses >= sign
        /* if (score >= highScore), 
        {
            highScore = score;
            Save(highScore);
        }*/
    }
    // call this once in start to create a saveFile
    public static void Load(string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            System.IO.FileStream saveFile =
                File.Open($"{Application.persistentDataPath}/{name}.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(saveFile);
            saveFile.Close();
            highScore = data.highScore;
        }
        catch (FileNotFoundException e)
        {
            //create a save file
            Save(0f, name);
        }
    }
}

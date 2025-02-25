using System;

[Serializable]
public class PlayerData
{
    public float highScore;
    public PlayerData(float highScore)
    {
        this.highScore = highScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> Score;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        playerPosition = Vector3.zero;
        Score = new SerializableDictionary<string, bool>();
    }

    public int GetPercentageComplete() 
    {
        int totalScore = 0;
        foreach (bool collected in Score.Values) 
        {
            if (collected) 
            {
                totalScore++;
            }
        }

        // ensure we don't divide by 0 when calculating the percentage
        int percentageCompleted = -1;
        if (Score.Count != 0) 
        {
            percentageCompleted = (totalScore * 100 / Score.Count);
        }
        return percentageCompleted;
    }
}

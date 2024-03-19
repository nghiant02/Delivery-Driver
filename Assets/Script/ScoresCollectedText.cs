using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreText : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int totalScore = 0;

    private int Score = 0;

    private TextMeshProUGUI scoreText;

    private void Awake() 
    {
        scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        // subscribe to events
        GameEventsManager.instance.onScoreCollected += OnScoreCollected;
    }

    public void LoadData(GameData data) 
    {
        foreach(KeyValuePair<string, bool> pair in data.Score) 
        {
            if (pair.Value) 
            {
                Score++;
            }
        }
    }

    public void SaveData(GameData data)
    {
        // no data needs to be saved for this script
    }

    private void OnDestroy() 
    {
        // unsubscribe from events
        GameEventsManager.instance.onScoreCollected -= OnScoreCollected;
    }

    private void OnScoreCollected() 
    {
        Score++;
    }

    private void Update() 
    {
        scoreText.text = Score + " / " + totalScore;
    }
}

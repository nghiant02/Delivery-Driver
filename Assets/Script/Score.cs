using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid() 
    {
        id = System.Guid.NewGuid().ToString();
    }

    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }

    public void LoadData(GameData data) 
    {
        data.Score.TryGetValue(id, out collected);
        if (collected) 
        {
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data) 
    {
        if (data.Score.ContainsKey(id))
        {
            data.Score.Remove(id);
        }
        data.Score.Add(id, collected);
    }

}

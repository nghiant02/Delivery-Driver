using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int score;
    public int packagesReceived;

    public GameData(int score, int packagesReceived)
    {
        this.score = score;
        this.packagesReceived = packagesReceived;
    }

    public static void SaveDataToJson()
    {
        GameData data = new GameData(Delivery.Score, Delivery.PackagesReceived);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static void LoadDataFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Use 'data.score' and 'data.packagesReceived' to restore game state
        }
    }
}

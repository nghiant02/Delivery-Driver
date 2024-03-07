using UnityEngine;
using System.IO;

public class SaveScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject saveLoadPanel; // Assign this in the inspector

    private void Update()
    {
        // Listen for the ESC key to toggle the save/load panel visibility
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSaveLoadPanel();
        }
    }

    private void ToggleSaveLoadPanel()
    {
        bool isActive = saveLoadPanel.activeSelf;
        saveLoadPanel.SetActive(!isActive); // Toggle the active state

        if (isActive)
        {
            // If the panel was active (now hidden), unpause the game
            Time.timeScale = 1f;
        }
        else
        {
            // If the panel was hidden (now active), pause the game
            Time.timeScale = 0f;
        }
    }

    public void SaveGame()
    {
        // Example save logic; replace with your actual game data serialization
        GameData data = new GameData(Delivery.Score, Delivery.PackagesReceived);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Game Saved");

        // After saving, hide the panel and unpause the game
        saveLoadPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CancelSave()
    {
        // Simply hide the panel and unpause the game
        saveLoadPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}

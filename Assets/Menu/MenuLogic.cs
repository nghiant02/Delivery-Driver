using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuLogic : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Setting()
    {
        throw new NotImplementedException();
    }
    public void Exit()
    {
        Application.Quit();
    }
}

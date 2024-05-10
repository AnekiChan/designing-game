using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static StartGameType gameType = StartGameType.Load;

    public void StartNewGame()
    {
        gameType = StartGameType.New;
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadGame()
    {
        gameType = StartGameType.Load;
		SceneManager.LoadScene("Gameplay");
	}

    public void QuitGame()
    {
		Application.Quit();
	}
}

public enum StartGameType
{
    New,
    Load
}
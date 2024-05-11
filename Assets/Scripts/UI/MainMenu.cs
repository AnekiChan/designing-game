using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static StartGameType gameType = StartGameType.Load;
    [SerializeField] private Animator transition;

    public void StartNewGame()
	{
		gameType = StartGameType.New;
		StartCoroutine(WaitBeforeStart());
    }

    public void LoadGame()
    {
		gameType = StartGameType.Load;
		StartCoroutine(WaitBeforeStart());
	}

    public void QuitGame()
    {
		Application.Quit();
	}

    private IEnumerator WaitBeforeStart()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Gameplay");
	}
}

public enum StartGameType
{
    New,
    Load
}
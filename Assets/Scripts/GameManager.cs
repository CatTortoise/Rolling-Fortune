using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public static void StartGame()
	{
		SceneManager.LoadSceneAsync("Combined Levels", LoadSceneMode.Single);
	}

	public static void ExitToMainMenu()
	{
		DOTween.KillAll();
		SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
	}

	public static void ExitGame()
	{
		Application.Quit();
	}
}

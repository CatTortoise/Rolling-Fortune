using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private LevelTransitions _levelTransitions;
	[SerializeField] private UnityAnalyticsManager _analytics;

	private void Update()
	{
		if (!_player.gameObject.activeSelf)
		{
			if(_player.CurrentLives > 0)
			{
				_player.Respond();
			}
			else
			{
				_analytics.LevelComplete(0, _player.CurrentLives, false);
			}
		}
	}

	public static void StartGame()
	{
		SceneManager.LoadSceneAsync("Combined Levels", LoadSceneMode.Single);
	}

	public static void ExitToMainMenu()
	{
		SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
	}

	public static void ExitGame()
	{
		Application.Quit();
	}
}

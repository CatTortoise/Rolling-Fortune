using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
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
			PauseManager.Instance.SetPaused(false);
			SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Single);
		}

		public static void ExitToMainMenu()
		{
			DOTween.KillAll();
			LevelTransitions.Instance.OnReturnToMainMenu();
			SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
		}

		public static void ExitGame()
		{
			Application.Quit();
		}
	}
}

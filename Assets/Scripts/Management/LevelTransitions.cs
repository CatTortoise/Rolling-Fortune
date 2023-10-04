using UnityEngine;
using UI;

namespace Management
{
	public class LevelTransitions : MonoBehaviour
	{
		private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
		private GameObject _currentLevelAsset;
		private GameObject _currentLevelInstance;
		[SerializeField] private PlayerScoreScriptableObject _playerScore;
		[SerializeField] private Transform _spawnLevelHere;

		public static LevelTransitions Instance { get; private set; }
		public int CurrentLevelIndex { get; private set; }

		private void Awake()
		{
			CurrentLevelIndex = 0;
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(this);
			}
		}

		private void Start()
		{
			LoadIntoNextLevel();
		}

		public void RestartCurrentLevel()
		{
			if (_currentLevelInstance != null)
				Destroy(_currentLevelInstance);
			_currentLevelInstance = Instantiate(_currentLevelAsset, _spawnLevelHere.transform);
			_playerScore.ResetScore();
			PauseManager.Instance.SetPaused(false);
			MenuManager.Instance.ShowInGameMenu();
		}

		public void LoadIntoNextLevel()
		{
			CurrentLevelIndex++;
			LoadIntoLevel(CurrentLevelIndex);
		}

		public void LoadIntoLevel(int level)
		{
			GameObject nextLevel = LoadLevelAsset(level);
			if (nextLevel != null)
			{
				_currentLevelAsset = nextLevel;
				RestartCurrentLevel();
			}
		}

		private GameObject LoadLevelAsset(int levelIndex)
		{
			GameObject level = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + levelIndex);
			if (level != null)
				Debug.Log("Loaded level.");
			else
				Debug.Log("Failed to load level.");

			return level;
		}
	}
}

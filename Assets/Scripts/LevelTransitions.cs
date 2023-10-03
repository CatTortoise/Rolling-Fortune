using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
	private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
	private int _currentLevelIndex = 0;
	private GameObject _currentLevelAsset;
	private GameObject _currentLevelInstance;
	[SerializeField] private PlayerScoreScriptableObject _playerScore;
	[SerializeField] private Transform _spawnLevelHere;

	public static LevelTransitions Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		LoadIntoNextLevel();
	}

	public void LoadIntoNextLevel()
	{
		_currentLevelIndex++;
		LoadIntoLevel(_currentLevelIndex);
	}

	public void LoadIntoLevel(int level)
	{
		GameObject nextLevel = LoadLevelAsset(level);
		if (nextLevel != null)
		{
			if (_currentLevelInstance != null)
				Destroy(_currentLevelInstance);
			_currentLevelAsset = nextLevel;
			_currentLevelInstance = Instantiate(_currentLevelAsset, _spawnLevelHere.transform);
			_playerScore.ResetScore();
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

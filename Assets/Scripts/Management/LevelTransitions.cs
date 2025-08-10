using UnityEngine;
using Unity.Scenes;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Entities.Serialization;
using UI;

namespace Management
{
	public class LevelTransitions : MonoBehaviour
	{
		public static LevelTransitions Instance { get; private set; }

		[SerializeField] private LevelCollection _levels;
		private EntitySceneReference CurrentLevel => _levels[math.min(CurrentLevelIndex, _levels.Count-1)];
		private Entity _currentLevelInstance;

		public int CurrentLevelIndex { get; private set; }

		private void Awake()
		{
			CurrentLevelIndex = -1;
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
			LoadLevel(CurrentLevel);
			PauseManager.Instance.SetPaused(false);
			MenuManager.Instance.ShowInGameMenu(true);
		}

		public void LoadIntoNextLevel()
		{
			CurrentLevelIndex++;
			LoadLevel(CurrentLevel);
			MenuManager.Instance.ShowInGameMenu(true);
		}

		private Entity LoadLevel(EntitySceneReference level)
		{
			TryDisposeCurrent();
			return _currentLevelInstance = SceneSystem.LoadSceneAsync(World.DefaultGameObjectInjectionWorld.Unmanaged, level);
		}

		public void OnReturnToMainMenu() => TryDisposeCurrent();

		private bool TryDisposeCurrent()
		{
			bool exists = World.DefaultGameObjectInjectionWorld.EntityManager.Exists(_currentLevelInstance);
			if (exists)
				World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(_currentLevelInstance);
			return exists;
		}
	}
}

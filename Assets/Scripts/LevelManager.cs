using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private EscapeHatch _escapeHatch;

	private void OnValidate()
	{
		if (_escapeHatch != null)
			_escapeHatch.gameObject.SetActive(false);
	}

	public void OnPlayerDeath()
	{
		_player.OnDeath();
		MenuManager.Instance.ShowDeathMenu();
		UnityAnalyticsManager.Instance.LevelComplete(LevelTransitions.Instance.CurrentLevelIndex, _player.CurrentLives, false);
	}

	public void OnAllGemsCollected()
	{
		_escapeHatch.OpenHatch();
	}

	public void OnLevelComplete()
	{
		_player.OnLevelComplete(_escapeHatch.transform);
		MenuManager.Instance.ShowLevelCompleteMenu();
		UnityAnalyticsManager.Instance.LevelComplete(LevelTransitions.Instance.CurrentLevelIndex, _player.CurrentLives, true);
	}
}

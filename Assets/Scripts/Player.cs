using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Collider _collider;
	[SerializeField] private int maxLives;

    public int CurrentLives { get; private set; }

    private void Start()
    {
        CurrentLives = maxLives;
    }

    public void OnEndLevel(Transform escapeHatchTransform)
    {
        _collider.enabled = false;
		transform.DOMove(escapeHatchTransform.position, 2).SetEase(Ease.OutCirc).OnComplete(() => LevelTransitions.Instance.LoadIntoNextLevel());
        UnityAnalyticsManager.Instance.LevelComplete(LevelTransitions.Instance.CurrentLevelIndex, CurrentLives, true);
    }

    public void OnDeath()
    {
        CurrentLives--;
        LevelTransitions.Instance.RestartCurrentLevel();
        UnityAnalyticsManager.Instance.LevelComplete(LevelTransitions.Instance.CurrentLevelIndex, CurrentLives, false);
	}
}

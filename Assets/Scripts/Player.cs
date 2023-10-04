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

    public void OnLevelComplete(Transform escapeHatchTransform)
    {
        _collider.enabled = false;
		transform.DOMove(escapeHatchTransform.position, 2).SetEase(Ease.OutCirc);
    }

    public void OnDeath()
	{
		_collider.enabled = false;
		CurrentLives--;
        transform.DOScale(0, 0.5f).SetEase(Ease.InCirc);
	}
}

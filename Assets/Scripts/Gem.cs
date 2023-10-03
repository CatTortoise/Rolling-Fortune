using UnityEngine;

public class Gem : MonoBehaviour
{
	[SerializeField] private PlayerScoreScriptableObject _playerScore;
	[SerializeField] private GemManager _gemManager;
	[SerializeField] private int _scoreForPickup = 5;
	[SerializeField] private string _triggerByTag;

	public int ScoreForPickup { get => _scoreForPickup; }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(_triggerByTag))
			CollectGem();
	}

	private void CollectGem()
	{
		_playerScore.RaiseScore(ScoreForPickup);
		_gemManager.GemCollected(this);
		gameObject.SetActive(false);
	}
}

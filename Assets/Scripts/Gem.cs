using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
	[SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int scoreForPickup = 5;

	public void OnCollect(Player collector)
    {
        _playerScore.RaiseScore(scoreForPickup);
    }
}

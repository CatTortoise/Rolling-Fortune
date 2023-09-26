using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
	[SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int scoreForPickup = 5;
    [SerializeField] private LevelTransitions _transitions;

	public void OnCollect(Player collector)
    {
        _playerScore.RaiseScore(scoreForPickup);
        _transitions.GemCollected(this);
        Destroy(this);
    }
}

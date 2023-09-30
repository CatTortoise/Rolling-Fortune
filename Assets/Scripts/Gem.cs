using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int _scoreForPickup = 5;
    [SerializeField] private string _triggerByTag;
    [SerializeField] private GemManager gemManagerRef;

    public int ScoreForPickup { get => _scoreForPickup; private set => _scoreForPickup = value; }

    private void Awake()
    {
        gemManagerRef.AddGemToList(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
        {
            gameObject.SetActive(false);
            gemManagerRef.GemCollected(this);
        }
    }
}

using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int _scoreForPickup = 5;
    [SerializeField] private string _triggerByTag;

    public int ScoreForPickup { get => _scoreForPickup;private set => _scoreForPickup = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(_triggerByTag))
        {
            gameObject.SetActive(false);
        }
    }


}

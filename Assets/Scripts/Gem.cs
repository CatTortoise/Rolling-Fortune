using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int scoreForPickup = 5;
    [SerializeField] private string triggerByTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(triggerByTag))
        {
            gameObject.SetActive(false);
        }
    }


}

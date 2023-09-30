using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private PlayerScoreScriptableObject _playerScore;
    [SerializeField] private int _scoreForPickup = 5;
    [SerializeField] private string _triggerByTag;
    //private GemManager _gemManagerRef;

    public int ScoreForPickup { get => _scoreForPickup; private set => _scoreForPickup = value; }
  //public GemManager GemManagerRef { get => _gemManagerRef; private set => _gemManagerRef = value; }
    
    /* private void Awake()
     {
         gemManagerRef.AddGemToList(this);
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
        {
            gameObject.SetActive(false);
            //_gemManagerRef.GemCollected(gameObject);
        }
    }
}

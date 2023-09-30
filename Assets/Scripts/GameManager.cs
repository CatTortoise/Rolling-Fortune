using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Gem[] _gems;
	[SerializeField] private Player _player;
    [SerializeField] private LevelTransitions _levelTransitions;
    [SerializeField] private UnityAnalyticsManager _analytics;
    private bool _allGemsCollected;
    
    private void Start()
    {
        _allGemsCollected = false;
    }


    private void Update()
    {
        if (!_player.gameObject.activeSelf)
        {
            if(_player.CurrentLives > 0)
            {
                _player.Respond();
            }
        }
        for (int i = 0; i < _gems.Length; i++)
        {
            if(_gems[i].gameObject.activeSelf)
            {
                _allGemsCollected = false;
                break;
            }
            else
            {
                _allGemsCollected = true;
            }
        }
    }

    public static void ExitGame()
	{
		Application.Quit();
	}

}

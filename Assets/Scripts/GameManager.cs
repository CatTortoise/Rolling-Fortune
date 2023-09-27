using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Gem[] _gems;
	[SerializeField] private Player _player;

    private void Update()
    {
        if (!_player.gameObject.activeSelf)
        {
            if(_player.CurrentLives > 0)
            {
                _player.Respond();
            }
        }
       
    }

    public static void ExitGame()
	{
		Application.Quit();
	}
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
	[SerializeField] private GameObject _gameBoard;

	private void FixedUpdate()
	{
		_gameBoard.transform.rotation = Quaternion.Euler(_inputManager.tiltVectorCumulative);
	}
}

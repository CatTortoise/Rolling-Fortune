using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private PlayerInput _playerInput;
	private InputActionMap _playerActionMap;
	private bool _paused = false;

	private void Start()
	{
		_playerActionMap = _playerInput.actions.FindActionMap("Player");
		_playerInput.actions.FindAction("Pause").Enable();
		ForceUnPause();
	}

	public void OnPause()
	{
		ToggleState();
		ForceCurrentState();
	}

	public void ForcePause()
	{
		_paused = true;
		_inGameMenu.SetActive(false);
		_pauseMenu.SetActive(true);
		_playerActionMap.Disable();
	}

	public void ForceUnPause()
	{
		_paused = false;
		_inGameMenu.SetActive(true);
		_pauseMenu.SetActive(false);
		_playerActionMap.Enable();
	}

	private void ForceCurrentState()
	{
		if (_paused)
			ForcePause();
		else
			ForceUnPause();
	}

	private void ToggleState()
	{
		_paused = !_paused;
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
			ForcePause();
	}
}

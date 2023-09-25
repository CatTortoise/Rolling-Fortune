using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private PlayerInput _playerInput;
	private bool _paused = false;

	private void Start()
	{
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
		_playerInput.DeactivateInput();
	}

	public void ForceUnPause()
	{
		_paused = false;
		_inGameMenu.SetActive(true);
		_pauseMenu.SetActive(false);
		_playerInput.ActivateInput();
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

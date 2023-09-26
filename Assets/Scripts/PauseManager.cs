using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private GameObject[] _objectsToToggle;
	[SerializeField] private CameraPause _camera;
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
		_camera.SetPaused(true);
		_inGameMenu.SetActive(false);
		_pauseMenu.SetActive(true);
		SetAllObjects(false);
		_playerActionMap.Disable();
	}

	public void ForceUnPause()
	{
		_paused = false;
		_camera.SetPaused(false);
		_inGameMenu.SetActive(true);
		_pauseMenu.SetActive(false);
		SetAllObjects(true);
		_playerActionMap.Enable();
	}

	private void ForceCurrentState()
	{
		if (_paused)
			ForcePause();
		else
			ForceUnPause();
	}

	private void SetAllObjects(bool active)
	{
		foreach (var obj in _objectsToToggle)
			obj.SetActive(active);
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

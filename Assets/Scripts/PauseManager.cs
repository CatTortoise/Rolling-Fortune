using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private CameraPause _camera;
	[SerializeField] private GameObject[] _objectsToToggle;
	private InputActionMap _playerActionMap;
	private bool _paused = false;

	private void OnValidate()
	{
		_objectsToToggle = GameObject.FindGameObjectsWithTag("Pauseable");
		_playerInput = FindAnyObjectByType<PlayerInput>();
		_camera = FindAnyObjectByType<CameraPause>();
	}

	private void Start()
	{
		_playerActionMap = _playerInput.actions.FindActionMap("Player");
		_playerInput.actions.FindAction("Pause").Enable();
		SetPaused(false);
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			ToggleState();
			ForceCurrentState();
		}
	}

	public void SetPaused(bool paused)
	{
		_paused = paused;
		_camera.SetPaused(paused);
		_inGameMenu.SetActive(!paused);
		_pauseMenu.SetActive(paused);
		SetAllObjects(!paused);
		SetInputAction(!paused);
	}

	private void ForceCurrentState()
	{
		SetPaused(_paused);
	}

	private void SetAllObjects(bool active)
	{
		foreach (var obj in _objectsToToggle)
			obj.SetActive(active);
	}

	private void SetInputAction(bool active)
	{
		if (active)
			_playerActionMap.Enable();
		else
			_playerActionMap.Disable();
	}

	private void ToggleState()
	{
		_paused = !_paused;
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
			SetPaused(true);
	}
}

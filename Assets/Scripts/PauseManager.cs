using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private GameObject[] _objectsToToggle;
	[SerializeField] private Camera _camera;
	private CameraClearFlags _cameraClearFlags;
	private int _cameraCullingMask;
	private InputActionMap _playerActionMap;
	private bool _paused = false;

	private void Start()
	{
		SaveCameraParameters();
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
		StartCoroutine(FreezeCam());
		_inGameMenu.SetActive(false);
		_pauseMenu.SetActive(true);
		SetAllObjects(false);
		_playerActionMap.Disable();
	}

	public void ForceUnPause()
	{
		_paused = false;
		_inGameMenu.SetActive(true);
		_pauseMenu.SetActive(false);
		SetAllObjects(true);
		UnFreezeCam();
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

	private IEnumerator FreezeCam()
	{
		_camera.clearFlags = CameraClearFlags.Nothing;
		yield return null;
		_camera.cullingMask = 0;
	}

	private void UnFreezeCam()
	{
		_camera.clearFlags = _cameraClearFlags;
		_camera.cullingMask = _cameraCullingMask;
	}

	private void SaveCameraParameters()
	{
		_cameraClearFlags = _camera.clearFlags;
		_cameraCullingMask = _camera.cullingMask;
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
			ForcePause();
	}
}

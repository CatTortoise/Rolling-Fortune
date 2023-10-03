using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using OnScreenStick = UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class InputManager : MonoBehaviour
{
	[SerializeField] private OnScreenStick _onScreenJoystick;
	public PlayerInput _playerInput;
	public InputSystemUIInputModule InputModule;
	private InputActionMap _playerActionMap;

	public static InputManager Instance { get; private set; }

	public InputSource ActiveInput { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		_playerActionMap = _playerInput.actions.FindActionMap("Player");
		_playerInput.actions.FindAction("Pause").Enable();
		if (GyroAvailable())
			SetInputSource(InputSource.Gyroscope);
		else
			SetInputSource(InputSource.Gamepad);
	}

	public void SetInputSource(InputSource inputSource)
	{
		ActiveInput = inputSource;
		if (inputSource == InputSource.Gyroscope)
		{
			SetDeviceEnabled<Gyroscope>(true);
			SetDeviceEnabled<Gamepad>(false);
			_onScreenJoystick.gameObject.SetActive(false);
		}
		else if (inputSource == InputSource.Gamepad)
		{
			SetDeviceEnabled<Gyroscope>(false);
			SetDeviceEnabled<Gamepad>(true);
			_onScreenJoystick.gameObject.SetActive(true);
		}
	}

	public void SetPlayerInputAction(bool active)
	{
		if (active)
			_playerActionMap.Enable();
		else
			_playerActionMap.Disable();
	}

	private void SetDeviceEnabled<T>(bool enabled) where T : InputDevice
	{
		var inputDevices = _playerInput.user.pairedDevices;
		foreach (var device in inputDevices)
		{
			if (device is T)
			{
				if (enabled)
					InputSystem.EnableDevice(device);
				else
					InputSystem.DisableDevice(device);
			}
		}
	}

	private bool GyroAvailable()
	{
		var inputDevices = _playerInput.user.pairedDevices;
		foreach (var device in inputDevices)
		{
			if (device is Gyroscope)
				return true;
		}
		return false;
	}

	public enum InputSource
	{
		Gyroscope,
		Gamepad
	}
}

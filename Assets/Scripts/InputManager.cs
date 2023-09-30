using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using OnScreenStick = UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class InputManager : MonoBehaviour
{
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private GameObject _onScreenJoystick;

	public InputSource ActiveInput { get; private set; }

	private void Start()
	{
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
			_onScreenJoystick.SetActive(false);
		}
		else if (inputSource == InputSource.Gamepad)
		{
			SetDeviceEnabled<Gyroscope>(false);
			SetDeviceEnabled<Gamepad>(true);
			_onScreenJoystick.SetActive(true);
		}
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

	private void OnValidate()
	{
		_playerInput = FindAnyObjectByType<PlayerInput>();
		_onScreenJoystick = FindAnyObjectByType<OnScreenStick>().gameObject;
	}

	public enum InputSource
	{
		Gyroscope,
		Gamepad
	}
}

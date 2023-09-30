using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class InputManager : MonoBehaviour
{
	[SerializeField] private PlayerInput _playerInput;
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
		}
		else if (inputSource == InputSource.Gamepad)
		{
			SetDeviceEnabled<Gyroscope>(false);
			SetDeviceEnabled<Gamepad>(true);
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
	}

	public enum InputSource
	{
		Gyroscope,
		Gamepad
	}
}

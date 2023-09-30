using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField] private PlayerInput _playerInput;

	public void EnableGyro(bool enabled)
	{
		SetDeviceEnabled<UnityEngine.InputSystem.Gyroscope>(enabled);
	}

	public void EnableGamepad(bool enabled)
	{
		SetDeviceEnabled<Gamepad>(enabled);
	}

	private void SetDeviceEnabled<T>(bool enabled) where T: InputDevice
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

	private void OnValidate()
	{
		_playerInput = FindAnyObjectByType<PlayerInput>();
	}
}

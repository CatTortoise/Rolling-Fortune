using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using OnScreenStick = UnityEngine.InputSystem.OnScreen.OnScreenStick;

namespace Management
{
	public class InputManager : MonoBehaviour
	{
		public static Actions Actions => World.DefaultGameObjectInjectionWorld.EntityManager.UniversalQuery.GetSingleton<Input.InputActions>().actions;

		public static InputManager Instance { get; private set; }

		[SerializeField] private OnScreenStick _onScreenJoystick;

		public InputSource ActiveInput { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			Actions.UI.Pause.Enable();
			if (GyroAvailable())
				SetInputSource(InputSource.Gyroscope);
			else
				SetInputSource(InputSource.Gamepad);
		}

		public void SetInputSource(InputSource inputSource)
		{
			ActiveInput = inputSource;
			SetDeviceEnabled<Gyroscope>(inputSource == InputSource.Gyroscope);
			SetDeviceEnabled<Gamepad>(inputSource == InputSource.Gamepad);
			_onScreenJoystick.gameObject.SetActive(inputSource == InputSource.Gamepad);
		}

		public void SetPlayerInputAction(bool active)
		{
			if (active)
				Actions.Player.Enable();
			else
				Actions.Player.Disable();
		}

		private void SetDeviceEnabled<T>(bool enabled) where T : InputDevice
		{
			foreach (var device in Actions.devices.Value.OfType<T>())
			{
				if (enabled)
					InputSystem.EnableDevice(device);
				else
					InputSystem.DisableDevice(device);
			}
		}

		private bool GyroAvailable() => Actions.devices.Value.OfType<Gyroscope>().Any();

		public enum InputSource
		{
			Gyroscope,
			Gamepad
		}
	}
}

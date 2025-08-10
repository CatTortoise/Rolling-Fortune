using System.Linq;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using OnScreenStick = UnityEngine.InputSystem.OnScreen.OnScreenStick;

namespace Management
{
	public class InputManager : MonoBehaviour
	{
		public Actions Actions => World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<Actions>(_inputSystem);

		public static InputManager Instance { get; private set; }

		[SerializeField] private OnScreenStick _onScreenJoystick;
		private SystemHandle _inputSystem;

		public InputSource ActiveInput { get; private set; }

		private void Awake()
		{
			if (!Instance)
				Instance = this;
			_inputSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<Input.InputSystem>();
		}

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
			foreach (var device in Actions.devices.OfType<T>())
			{
				if (enabled)
					InputSystem.EnableDevice(device);
				else
					InputSystem.DisableDevice(device);
			}
		}

		private bool GyroAvailable() => Actions.devices.OfType<Gyroscope>().Any();

		public enum InputSource
		{
			Gyroscope,
			Gamepad
		}
	}
}

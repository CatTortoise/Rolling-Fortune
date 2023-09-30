using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager;
	[SerializeField] private Toggle _enableGamepad;

	private void Start()
	{
		_enableGamepad.isOn = _inputManager.ActiveInput == InputManager.InputSource.Gamepad;
	}

	public void UseGamepad(bool value)
	{
		_inputManager.SetInputSource(value ? InputManager.InputSource.Gamepad : InputManager.InputSource.Gyroscope);
	}

	private void OnValidate()
	{
		_inputManager = FindAnyObjectByType<InputManager>();
		_enableGamepad = GetComponentInChildren<Toggle>();
	}
}

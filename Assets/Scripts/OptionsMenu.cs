using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField] private Toggle _enableGamepad;

	private void Start()
	{
		_enableGamepad.isOn = InputManager.Instance.ActiveInput == InputManager.InputSource.Gamepad;
	}

	public void UseGamepad(bool value)
	{
		InputManager.Instance.SetInputSource(value ? InputManager.InputSource.Gamepad : InputManager.InputSource.Gyroscope);
	}

	private void OnValidate()
	{
		_enableGamepad = GetComponentInChildren<Toggle>();
	}
}

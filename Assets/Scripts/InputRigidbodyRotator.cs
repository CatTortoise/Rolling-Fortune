using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputRigidbodyRotator : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private float _rotationLimit = 180;
	private Vector3 _rotation = Vector3.zero;

	private void OnValidate()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		EnableControls();
	}

	private void FixedUpdate()
	{
		ApplyRotationToRigidbody();
	}

	public void OnTilt(InputAction.CallbackContext value)
	{
		_rotation = TiltToRotation(value.ReadValue<Vector2>());
		Normalize();
	}

	public void OnResetTilt()
	{
		_rotation = Vector3.zero;
	}

	private void ApplyRotationToRigidbody()
	{
		_rigidbody.rotation = Quaternion.Euler(_rotation);
	}

	private void Normalize()
	{
		_rotation *= _rotationLimit;
	}

	private Vector3 TiltToRotation(Vector2 tiltVector)
	{
		return new(tiltVector.x, 0, tiltVector.y);
	}

	private void EnableControls()
	{
		foreach (var device in _playerInput.user.pairedDevices)
			InputSystem.EnableDevice(device);
	}

	private void DisableControls()
	{
		foreach (var device in _playerInput.user.pairedDevices)
			InputSystem.DisableDevice(device);
	}
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputRigidbodyRotator : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private float _rotationLimit = 180;
	private Vector3 _rotation = Vector3.zero;

	private Vector3 ScaledRotation { get => _rotation * _rotationLimit; }

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
		ClampRotation();
		ApplyRotationToRigidbody();
	}

	public void OnTiltCumulative(InputAction.CallbackContext value)
	{
		if (value.performed)
			_rotation = TiltToRotation(value.ReadValue<Vector2>());
		else if (value.canceled)
			OnResetTilt();
	}

	public void OnTiltDelta(InputAction.CallbackContext value)
	{
		if (value.performed)
			_rotation += TiltToRotation(value.ReadValue<Vector2>());
	}

	public void OnResetTilt()
	{
		_rotation = Vector3.zero;
	}

	private void ApplyRotationToRigidbody()
	{
		_rigidbody.rotation = Quaternion.Euler(ScaledRotation);
	}

	private void ClampRotation()
	{
		_rotation = ClampRotation(_rotation, 1);
	}

	private static Vector3 ClampRotation(Vector3 rotation, float clampTo)
	{
		if (rotation.magnitude > clampTo)
			rotation = rotation.normalized * clampTo;
		return rotation;
	}

	private static Vector3 TiltToRotation(Vector2 tiltVector)
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

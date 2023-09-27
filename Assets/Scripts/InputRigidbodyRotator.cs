using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputRigidbodyRotator : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private bool _limitRotation;
	[SerializeField] private float _rotationLimit = 180;
	private Vector3 _tiltDelta = Vector3.zero;

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
		ApplyRigidbodyRotation();
	}

	public void OnTilt(InputAction.CallbackContext value)
	{
		_tiltDelta += TiltToRotation(value.ReadValue<Vector2>());
		if (_limitRotation)
			ApplyRotationLimit();
	}

	public void OnResetTilt()
	{
		_tiltDelta = Vector3.zero;
	}

	private void ApplyRigidbodyRotation()
	{
		_rigidbody.rotation = Quaternion.Euler(_tiltDelta);
	}

	private void ApplyRotationLimit()
	{
		_tiltDelta = new(ClampAngle(_tiltDelta.x), ClampAngle(_tiltDelta.y), ClampAngle(_tiltDelta.z));
	}

	private Vector3 TiltToRotation(Vector2 gyroVector)
	{
		return new(gyroVector.x, 0, gyroVector.y);
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

	private float ClampAngle(float angle)
	{
		return Mathf.Clamp(OffsetEulerAngle(angle), -_rotationLimit, _rotationLimit);
	}

	private static float OffsetEulerAngle(float angle)
	{
		return angle < 180 ? angle : angle - 360;
	}
}

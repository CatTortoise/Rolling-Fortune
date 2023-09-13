using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class InputRigidbodyRotator : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidBody;
	[SerializeField] private PlayerInput _playerInput;
	private Vector3 _initialPosition;
	private Vector3 _initialRotation;
	private Vector3 _tiltDelta = Vector3.zero;

	private void OnValidate()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_playerInput = GetComponent<PlayerInput>();
	}

	private void Awake()
	{
		_initialPosition = _rigidBody.position;
		_initialRotation = _rigidBody.rotation.eulerAngles;
	}

	private void OnEnable()
	{
		EnableControls();
	}

	private void OnDisable()
	{
		DisableControls();
	}

	private void FixedUpdate()
	{
		ApplyRigidbodyRotation();
	}

	public void OnTilt(InputAction.CallbackContext value)
	{
		_tiltDelta += TiltToRotation(value.ReadValue<Vector2>());
	}

	public void OnResetTilt()
	{
		_tiltDelta = Vector3.zero;
	}

	private void ApplyRigidbodyRotation()
	{
		_rigidBody.rotation = Quaternion.Euler(_tiltDelta);
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
}

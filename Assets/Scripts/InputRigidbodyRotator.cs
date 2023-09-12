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

	private void OnTilt(InputValue value)
	{
		_tiltDelta += TiltToRotation(value.Get<Vector2>());
	}

	private void ApplyRigidbodyRotation()
	{
		_rigidBody.rotation = Quaternion.Euler(_tiltDelta);
	}

	public void ResetTilt()
	{
		_tiltDelta = Vector3.zero;
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

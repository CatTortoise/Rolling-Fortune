using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager;
	[SerializeField] private Rigidbody _rigidBody;

	private Vector3 InputTilt { get => _inputManager.tiltVectorCumulative; }

	private void FixedUpdate()
	{
		ApplyBoardRotation();
	}

	private void ApplyBoardRotation()
	{
		_rigidBody.rotation = Quaternion.Euler(InputTilt);
	}

	private void OnValidate()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}
}

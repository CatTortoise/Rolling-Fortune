using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager;
	private Transform _transform;

	private Vector3 InputTilt { get => _inputManager.tiltVectorCumulative; }

	private void Awake()
	{
		_transform = transform;
	}

	private void FixedUpdate()
	{
		UpdateBoardRotation();
	}

	private void UpdateBoardRotation()
	{
		_transform.rotation = Quaternion.Euler(InputTilt);
	}
}

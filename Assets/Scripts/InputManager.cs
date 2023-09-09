using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
	[System.NonSerialized] public Vector3 tiltVectorDelta = Vector3.zero;
	[System.NonSerialized] public Vector3 tiltVectorCumulative = Vector3.zero;

	private void Start()
	{
		EnableGyroControls();
	}

	private void OnTilt(InputValue value)
	{
		var vec = TiltToRotation(value.Get<Vector2>());
		tiltVectorDelta = vec;
		tiltVectorCumulative += vec;
	}

	public void ResetGyro()
	{
		tiltVectorCumulative = Vector3.zero;
	}

	private Vector3 TiltToRotation(Vector2 gyroVector)
	{
		return new Vector3(gyroVector.x, 0, gyroVector.y);
	}

	private static void EnableGyroControls()
	{
		if (UnityEngine.InputSystem.Gyroscope.current != null)
			InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
	}
}

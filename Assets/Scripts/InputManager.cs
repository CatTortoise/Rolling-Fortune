using TMPro;
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
		var vec = NormalizeGyro(value.Get<Vector3>());
		tiltVectorDelta = vec;
		tiltVectorCumulative += vec;
	}

	public void ResetGyro()
	{
		tiltVectorCumulative = Vector3.zero;
	}

	private Vector3 NormalizeGyro(Vector3 gyroVector)
	{
		return new Vector3(gyroVector.x, gyroVector.z, gyroVector.y);
	}

	private static void EnableGyroControls()
	{
		InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
	}
}

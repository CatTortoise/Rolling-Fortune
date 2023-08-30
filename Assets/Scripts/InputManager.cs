using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
	[System.NonSerialized] public Vector3 tiltVector = Vector3.zero;

	private void OnTilt(InputValue value)
	{
		var vec = value.Get<Vector3>();
		tiltVector = vec;
	}
}

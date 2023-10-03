using UnityEngine;
using UnityEngine.InputSystem;

public class InputCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxFov = 90;
    [SerializeField] private float _minFov = 30;
    private float _fovDiff;
	private float _currentFoVCoefficient;

	private void Awake()
	{
		_fovDiff = _maxFov - _minFov;
	}

	private void Start()
	{
		_currentFoVCoefficient = (_camera.fieldOfView - _minFov)/_fovDiff;
	}

	public void OnCameraZoom(InputAction.CallbackContext value)
    {
		if (value.performed)
		{
			_currentFoVCoefficient += value.ReadValue<float>();
			_currentFoVCoefficient = Mathf.Clamp01(_currentFoVCoefficient);
			_camera.fieldOfView = _minFov + (_fovDiff * _currentFoVCoefficient);
		}
    }
}

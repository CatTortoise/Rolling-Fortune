using UnityEngine;
using UnityEngine.InputSystem;

public class InputCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxFov = 90;
    [SerializeField] private float _minFov = 30;
    private float _fovDiff;
	public float CurrentFoVCoefficient { get; private set; }

	private void Awake()
	{
		_fovDiff = _maxFov - _minFov;
	}

	private void Start()
	{
		CurrentFoVCoefficient = (_camera.fieldOfView - _minFov)/_fovDiff;
	}

	public void OnCameraZoom(InputAction.CallbackContext value)
    {
		if (value.performed)
			ChangeZoom(CurrentFoVCoefficient + value.ReadValue<float>());
	}

	public void ChangeZoom(float zoom)
	{
		CurrentFoVCoefficient = zoom;
		UpdateZoom();
	}

	private void UpdateZoom()
	{
		CurrentFoVCoefficient = Mathf.Clamp01(CurrentFoVCoefficient);
		_camera.fieldOfView = _maxFov - (_fovDiff * CurrentFoVCoefficient);
	}
}

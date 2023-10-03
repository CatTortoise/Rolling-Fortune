using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private EscapeHatch _escapeHatch;

	private void OnValidate()
	{
		if (_escapeHatch != null)
			_escapeHatch.gameObject.SetActive(false);
	}

	private void Start()
	{
		CloseEscapeHatch();
	}

	public void OpenEscapeHatch()
	{
		_escapeHatch.OpenHatch();
	}

	public void CloseEscapeHatch()
	{
		_escapeHatch.CloseHatch();
	}
}

using UnityEngine;
using UnityEngine.InputSystem;
using UI;

namespace Management
{
	public class PauseManager : MonoBehaviour
	{
		private bool _paused = false;

		public static PauseManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			SetPaused(false);
		}

		public void OnPause(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				ToggleState();
				ForceCurrentState();
			}
		}

		public void SetPaused(bool paused)
		{
			_paused = paused;
			SetPlayerInputAction(!paused);
			ShowPauseMenu(paused);
		}

		private void ForceCurrentState()
		{
			SetPaused(_paused);
		}

		private void SetPlayerInputAction(bool active)
		{
			InputManager.Instance.SetPlayerInputAction(active);
		}

		private void ShowPauseMenu(bool pause)
		{
			if (pause)
				MenuManager.Instance.ShowPauseMenu();
			else
				MenuManager.Instance.ShowInGameMenu();
		}

		private void ToggleState()
		{
			_paused = !_paused;
		}

		private void OnApplicationFocus(bool focus)
		{
			if (!focus)
				SetPaused(true);
		}
	}
}

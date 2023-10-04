using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject _inGameMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private OptionsMenu _optionsMenu;
	[SerializeField] private EndLevelMenu _levelCompleteMenu;
	[SerializeField] private EndLevelMenu _deathMenu;

	public static MenuManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void DisableAll()
	{
		_inGameMenu.SetActive(false);
		_pauseMenu.SetActive(false);
		_optionsMenu.gameObject.SetActive(false);
		_levelCompleteMenu.gameObject.SetActive(false);
		_deathMenu.gameObject.SetActive(false);
	}

	public void ShowInGameMenu()
	{
		DisableAll();
		_inGameMenu.SetActive(true);
	}

	public void ShowPauseMenu()
	{
		DisableAll();
		_pauseMenu.SetActive(true);
	}

	public void ShowOptionsMenu()
	{
		DisableAll();
		_optionsMenu.gameObject.SetActive(true);
	}

	public void ShowLevelCompleteMenu()
	{
		DisableAll();
		_levelCompleteMenu.gameObject.SetActive(true);
	}

	public void ShowDeathMenu()
	{
		DisableAll();
		_deathMenu.gameObject.SetActive(true);
	}

	public void ExitToMainMenu()
	{
		GameManager.ExitToMainMenu();
	}
}

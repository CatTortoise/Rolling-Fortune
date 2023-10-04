using UnityEngine;
using DG.Tweening;
using TMPro;

public class EndLevelMenu : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private float _textDestinationSpacing = 0;
	[SerializeField] private float _textAnimationDuration = 3;
	private float _textDefaultSpacing;

	private void Awake()
	{
		_textDefaultSpacing = _text.characterSpacing;
	}

	private void OnEnable()
	{
		AnimateText();
	}

	public void NextLevel()
	{
		LevelTransitions.Instance.LoadIntoNextLevel();
	}

	public void RestartLevel()
	{
		LevelTransitions.Instance.RestartCurrentLevel();
	}

	public void ExitToMenu()
	{
		GameManager.ExitToMainMenu();
	}

	public void AnimateText()
	{
		DOTween.To(() => _text.characterSpacing,
			setter: x => _text.characterSpacing = x,
			endValue: _textDestinationSpacing,
			duration: _textAnimationDuration)
			.SetEase(Ease.Linear)
			.ChangeStartValue(_textDefaultSpacing);
	}
}

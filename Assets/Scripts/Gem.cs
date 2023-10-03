using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{
	[SerializeField] private PlayerScoreScriptableObject _playerScore;
	[SerializeField] private GemManager _gemManager;
	[SerializeField] private MeshRenderer _renderer;
	[SerializeField] private int _scoreForPickup = 5;
	[SerializeField] private string _triggerByTag;
	private Material _material;

	public int ScoreForPickup { get => _scoreForPickup; }

	private void Start()
	{
		_material = _renderer.material;
	}

	private void OnEnable()
	{
		DOIdle();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(_triggerByTag))
			CollectGem();
	}

	private void CollectGem()
	{
		_playerScore.RaiseScore(ScoreForPickup);
		_gemManager.GemCollected(this);
		DODisappear();
	}

	private void DOIdle()
	{
		transform.DOLocalMoveY(0.5f, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
		transform.DOLocalRotate(new(0, 360, 0), 5, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}

	private void DODisappear()
	{
		transform.DOKill();
		_material.DOColor(Color.gray, 0.25f);
		transform.DOLocalRotate(new(0, 360, 0), 0.75f, RotateMode.FastBeyond360);
		transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InElastic, 0.5f).OnComplete(() => gameObject.SetActive(false));
	}
}

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

	private void DODisappear()
	{
		_material.DOColor(Color.gray, 0.25f);
		transform.DOLocalRotate(new(0, 360, 0), 0.75f, RotateMode.FastBeyond360);
		transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InElastic, 0.5f).OnComplete(() => gameObject.SetActive(false));
	}
}

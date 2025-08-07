using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Management;

namespace InGame
{
	public class Gem : MonoBehaviour
	{
		[SerializeField] private GemManager _gemManager;
		[SerializeField] private MeshRenderer _renderer;
		[SerializeField] private string _triggerByTag;
		private Material _material;
		private Transform _graphicsTransform;

		private void Start()
		{
			_graphicsTransform = _renderer.transform;
			_material = _renderer.material;
			DOIdle();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag(_triggerByTag))
				CollectGem();
		}

		private void CollectGem()
		{
			_gemManager.GemCollected(this);
			DODisappear();
		}

		private void DOIdle()
		{
			_graphicsTransform.DOLocalMoveY(0.5f, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
			_graphicsTransform.DOLocalRotate(new(0, 360, 0), 5, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
		}

		private void DODisappear()
		{
			_graphicsTransform.DOKill();
			_material.DOColor(Color.gray, 0.25f);
			_graphicsTransform.DOLocalRotate(new(0, 360, 0), 0.75f, RotateMode.FastBeyond360);
			_graphicsTransform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InElastic, 0.5f).OnComplete(() => gameObject.SetActive(false));
		}
	}
}

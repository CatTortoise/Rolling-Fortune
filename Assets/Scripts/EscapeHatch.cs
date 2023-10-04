using DG.Tweening;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private string _triggerByTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
			_levelManager.OnLevelComplete();
    }

    public void OpenHatch()
	{
		gameObject.SetActive(true);
		DOAppear();
    }

	private void DOAppear()
	{
		transform.DOScale(transform.localScale, 1.5f).ChangeStartValue(new(0, 1, 0)).SetEase(Ease.InElastic, 0.5f);
	}
}

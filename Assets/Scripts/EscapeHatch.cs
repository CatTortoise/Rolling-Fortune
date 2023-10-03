using DG.Tweening;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private string _triggerByTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
			_player.OnEndLevel(transform);
    }

    public void OpenHatch()
	{
		gameObject.SetActive(true);
		DOAppear();
    }
    public void CloseHatch()
    {
        gameObject.SetActive(false);
	}

	private void DOAppear()
	{
		transform.DOScale(transform.localScale, 1.5f).ChangeStartValue(new(0, 1, 0)).SetEase(Ease.InElastic, 0.5f);
	}
}

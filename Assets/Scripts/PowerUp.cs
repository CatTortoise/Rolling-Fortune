using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectable
{
    [SerializeField] private Boulder[] _boulders;
	[SerializeField] private int freezeForSeconds = 5;

	public void OnCollect(Player collector)
    {
        foreach (var boulder in _boulders)
            boulder.FreezeFor(freezeForSeconds);
    }
}

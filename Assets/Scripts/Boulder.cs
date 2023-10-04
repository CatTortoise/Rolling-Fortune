using System;
using UnityEngine;

public class Boulder : MonoBehaviour
{
	[SerializeField] private LevelManager _levelManager;
	[SerializeField] private string triggerByTag;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag(triggerByTag))
			_levelManager.OnPlayerDeath();
	}

	public void FreezeFor(float seconds)
    {
        throw new NotImplementedException();
    }
}

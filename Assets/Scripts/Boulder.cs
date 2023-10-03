using System;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] private string triggerByTag;
    [SerializeField] private Player _player;

    public void FreezeFor(float seconds)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(triggerByTag))
			_player.OnDeath(); // TODO Display a "You died" screen
    }
}

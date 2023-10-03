using System;
using UnityEngine;

public class Boulder : MonoBehaviour
{

    [SerializeField] private string triggerByTag;

    public void FreezeFor(float seconds)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(triggerByTag))
            collision.collider.gameObject.SetActive(false); // TODO Display a "You died" screen
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
	{
		GameObject gameObject = other.gameObject;
		if (gameObject.CompareTag(ICollectable.COLLECTABLE_TAG))
			gameObject.GetComponent<ICollectable>().OnCollect(this);
	}
}

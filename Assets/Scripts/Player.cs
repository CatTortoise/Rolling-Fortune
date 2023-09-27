using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private int maxLives;
	private int _currentLives;
    private Vector3 _startingPosition;

    public int CurrentLives { get => _currentLives; private set => _currentLives = value; }

    private void Start()
    {
        CurrentLives = maxLives;
        _startingPosition = transform.position;
    }
    /*
    private void OnCollisionEnter(Collision other)
	{
		GameObject _gameObject = other.gameObject;
		Debug.Log($"I collided with {_gameObject.tag}");
		if (_gameObject.CompareTag(ICollectable.COLLECTABLE_TAG))
		{
			_gameObject.GetComponent<ICollectable>().OnCollect(this);
		}
	}*/
    public void Respond()
    {
        if (!gameObject.activeSelf)
        {
            transform.position = _startingPosition;
            gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {

        CurrentLives--;
    }
}

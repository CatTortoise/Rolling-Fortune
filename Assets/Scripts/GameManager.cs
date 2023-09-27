using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Gem[] _gems;

    public static void ExitGame()
	{
		Application.Quit();
	}
}

using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private string _triggerByTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
            LevelTransitions.Instance.LoadIntoNextLevel();
    }

    public void OpenHatch()
    {
        gameObject.SetActive(true);
    }
    public void CloseHatch()
    {
        gameObject.SetActive(false);
    }
}

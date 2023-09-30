using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private LevelTransitions _transitions;
    [SerializeField] private string _triggerByTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
        {
            //Add pause here
            //Pull up menu
            //Wait for button click / if needed move CompleteLevel to a different function
            Debug.Log("beep");
            _transitions.TransitionLevel();
        }
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

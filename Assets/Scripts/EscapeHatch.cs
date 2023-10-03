using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private LevelTransitions _transitions;
    [SerializeField] private string _triggerByTag;
    private bool _transitionLevel;

    private void Start()
    {
        _transitionLevel = false;
    }

    public bool TransitionLevel { get => _transitionLevel;private set => _transitionLevel = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_triggerByTag))
        {
            //Add pause here
            //Pull up menu
            //Wait for button click / if needed move CompleteLevel to a different function
           //Debug.Log("beep");
            TransitionLevel = true;
        }
    }

    public void OpenHatch()
    {
        gameObject.SetActive(true);
        TransitionLevel = false;
    }
    public void CloseHatch()
    {
        gameObject.SetActive(false);
        TransitionLevel = false;
    }
}

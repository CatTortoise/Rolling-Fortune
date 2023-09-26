using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private LevelTransitions _transitions;
    private void OnCollisionEnter(Collision collision)
    {
        //Add pause here
        _transitions.CompleteLevel();
    }
}

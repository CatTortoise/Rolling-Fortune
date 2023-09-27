using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeHatch : MonoBehaviour
{
    [SerializeField] private LevelTransitions _transitions;
    private void OnCollisionEnter(Collision collision) //Fix collision
    {
        if (collision.gameObject.name == "Player")
        {
            //Add pause here
            //Pull up menu
            //Wait for button click / if needed move CompleteLevel to a different function
            Debug.Log("beep");
            _transitions.TransitionLevel();
        }
    }
}

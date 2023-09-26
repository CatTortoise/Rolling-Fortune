using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField] private List<Gem> _gems;
    [SerializeField] private GameObject _escapeHatch;

    public bool HaveAllGemsBeenCollected { get => _gems.Count == 0; }

    public void GemCollected(Gem gem)
    {
        _gems.Remove(gem);
        if (HaveAllGemsBeenCollected)
        {
            _escapeHatch.SetActive(true);
        }
    }

    public void CompleteLevel()
    {
        //Implement
    }
}

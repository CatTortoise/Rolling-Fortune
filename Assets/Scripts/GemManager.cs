using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    [SerializeField] private List<Gem> gemList;
    [SerializeField] private LevelTransitions levelTransitionsRef;
    public bool HaveAllGemsBeenCollected = false;

    private void Awake()
    {
        gemList.Clear();
        HaveAllGemsBeenCollected = false;
    }

    public void AddGemToList(Gem gem)
    {
        gemList.Add(gem);
    }

    public void GemCollected(Gem gem)
    {
        gemList.Remove(gem);
        if (gemList.Count > 0 ) { HaveAllGemsBeenCollected = false; } else { HaveAllGemsBeenCollected = true; }
        if (HaveAllGemsBeenCollected)
        {
            levelTransitionsRef.LoadNextLevel();
        }
    }
}

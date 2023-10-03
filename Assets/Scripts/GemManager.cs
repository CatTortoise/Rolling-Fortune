using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    [SerializeField] private GameObject _diamondPrefab;
    [SerializeField] private List<Transform> _gemPosition;
    [SerializeField] private List<GameObject> _gemList;
    private bool _haveAllGemsBeenCollected = false;
    private bool _HalfOfAllGemsBeenCollected = false;

    public bool AllGemsBeenCollected { get => _haveAllGemsBeenCollected; private set => _haveAllGemsBeenCollected = value; }
    public bool HalfOfAllGemsBeenCollected { get => _HalfOfAllGemsBeenCollected; set => _HalfOfAllGemsBeenCollected = value; }

    private void Awake()
    {
        AllGemsBeenCollected = false;
        foreach (Transform gemTransform in _gemPosition)
        {
            _gemList.Add(Instantiate(_diamondPrefab, gemTransform));
        }
        LevelTransitions.Instance();
    }


    public bool GemCheck ()
    {
        if (!AllGemsBeenCollected)
        {
            foreach (GameObject gem in _gemList)
            {
                if (gameObject.activeSelf)
                {
                    AllGemsBeenCollected = false;
                    return AllGemsBeenCollected;
                }
            }
        }
        AllGemsBeenCollected = true;
        return AllGemsBeenCollected;
    }
}

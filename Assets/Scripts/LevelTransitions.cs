using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField] private List<Gem> _gems;
    [SerializeField] private GameObject _escapeHatch;
    [SerializeField] private GameObject[] lvlFolders;
    private int currentLevel = 1;
    [SerializeField] private GameObject player;
    private static Vector3 playerStartingPos;

    public bool HaveAllGemsBeenCollected { get => _gems.Count == 0; }

    private void Start()
    {
        playerStartingPos = player.transform.position;
    }

    public void GemCollected(Gem gem)
    {
        _gems.Remove(gem);
        if (HaveAllGemsBeenCollected)
        {
            _escapeHatch.SetActive(true);
            //Start loading the next level here
        }
    }

    public void TransitionLevel()
    {
        //Change transition to destroy and instantiate
        _escapeHatch.SetActive(false);
        lvlFolders[currentLevel - 1].SetActive(false);
        currentLevel++;
        lvlFolders[currentLevel - 1].SetActive(true);
        player.transform.position = playerStartingPos;
        //reset score
    }
}

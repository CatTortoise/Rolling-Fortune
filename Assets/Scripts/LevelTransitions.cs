using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Gem> _gems;
    [SerializeField] private GameObject _escapeHatch;
    [SerializeField] private GameObject[] lvlFolders;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject boardParent;
    private int currentLevelIndex = 1;
    private static Vector3 playerStartingPos;
    private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
    private GameObject tempLevelObject;

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
            tempLevelObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + (currentLevelIndex + 1));
            if (tempLevelObject != null) { Debug.Log("Loaded object"); }
            else { Debug.Log("Failed to load object"); }
        }
    }

    public void TransitionLevel()
    {
        Destroy(currentLevel);
        _escapeHatch.SetActive(false);
        currentLevelIndex++;
        currentLevel = Instantiate(tempLevelObject, boardParent.transform);
        currentLevel.SetActive(true);
        tempLevelObject = null; //clearing temp object after use
        player.transform.position = playerStartingPos;
        //reset score
    }
}

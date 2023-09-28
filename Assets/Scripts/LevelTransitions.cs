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
    [SerializeField] private GameObject boardParent;
    private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
    private GameObject tempLevelObject;

    public bool HaveAllGemsBeenCollected { get => _gems.Count == 0; }

    private void Start()
    {
        playerStartingPos = player.transform.position;
        Destroy(GameObject.Find("Level " + currentLevel));
        currentLevel++;
        tempLevelObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + currentLevel);
        if (tempLevelObject != null) { Debug.Log("Loaded object"); }
        else { Debug.Log("Failed to load object"); }
        Instantiate(tempLevelObject, boardParent.transform);
        tempLevelObject.name = tempLevelObject.name.Replace("Level " + currentLevel + "(Clone)", "Level " + currentLevel);
        tempLevelObject.SetActive(true);
        tempLevelObject = null; //clearing temp object after use
        player.transform.position = playerStartingPos;
    }

    public void GemCollected(Gem gem)
    {
        _gems.Remove(gem);
        if (HaveAllGemsBeenCollected)
        {
            _escapeHatch.SetActive(true);
            tempLevelObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + (currentLevel + 1));
            if (tempLevelObject != null) { Debug.Log("Loaded object"); }
            else { Debug.Log("Failed to load object"); }
        }
    }

    public void TransitionLevel()
    {
        Destroy(GameObject.Find("Level " + currentLevel));
        _escapeHatch.SetActive(false);
        currentLevel++;
        Instantiate(tempLevelObject);
        tempLevelObject.SetActive(true);
        tempLevelObject = null; //clearing temp object after use
        player.transform.position = playerStartingPos;
        //reset score
    }
}

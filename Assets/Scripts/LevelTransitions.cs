using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private EscapeHatch escapeHatch;
    [SerializeField] private GameObject[] lvlFolders;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject boardParent;
    private int currentLevelIndex = 1;
    private static Vector3 playerStartingPos;
    private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
    private GameObject tempLevelObject;

    private void Start()
    {
        playerStartingPos = player.transform.position;
    }

    public void LoadNextLevel()
    {
        escapeHatch.OpenHatch();
        tempLevelObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + (currentLevelIndex + 1));
        if (tempLevelObject != null) { Debug.Log("Loaded object"); }
        else { Debug.Log("Failed to load object"); }
    }

    public void TransitionLevel()
    {
        Destroy(currentLevel);
        escapeHatch.CloseHatch();
        currentLevelIndex++;
        currentLevel = Instantiate(tempLevelObject, boardParent.transform);
        currentLevel.SetActive(true);
        tempLevelObject = null; //clearing temp object after use
        player.transform.position = playerStartingPos;
        //reset score
    }
}

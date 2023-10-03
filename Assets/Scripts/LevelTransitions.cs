using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private EscapeHatch escapeHatch;
    [SerializeField] private GameObject currentLevelStage;
    [SerializeField] private GameObject currentLevelDiamonds;
    [SerializeField] private GameObject boardParent;
    private int currentLevelIndex = 1;
    private static Vector3 playerStartingPos;
    private const string RESOURCE_LEVEL_PATH = "Levels/Level ";
    private const string RESOURCE_STAGE_PATH = "/Stage";
    private const string RESOURCE_DIAMONDS_PATH = "/Diamonds";
    private GameObject tempStageObject;
    private GameObject tempDiamondsObject;
    private static LevelTransitions instance;

    public static LevelTransitions Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerStartingPos = player.transform.position;
    }


    private void LoadNextLevelStage()
    {
        tempStageObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + (currentLevelIndex + 1) + RESOURCE_STAGE_PATH);
        if (tempStageObject != null) { Debug.Log("Loaded object"); }
        else { Debug.Log("Failed to load object"); }
    }
    private void LoadNextLevelDiamonds()
    {
        tempDiamondsObject = (GameObject)Resources.Load(RESOURCE_LEVEL_PATH + (currentLevelIndex + 1) + RESOURCE_DIAMONDS_PATH);
        if (tempDiamondsObject != null) { Debug.Log("Loaded object"); }
        else { Debug.Log("Failed to load object"); }
    }

    public void OpenEscapeHatch()
    {
        escapeHatch.OpenHatch();
    }
    public void CloseEscapeHatch()
    {
        escapeHatch.CloseHatch();
    }

    private void TransitionLevel()
    {
        Destroy(currentLevelStage);
        Destroy(currentLevelDiamonds);
        escapeHatch.CloseHatch();
        currentLevelIndex++;
        currentLevelStage = Instantiate(tempStageObject, boardParent.transform);
        currentLevelDiamonds = Instantiate(tempDiamondsObject, boardParent.transform);
        currentLevelStage.SetActive(true);
        currentLevelDiamonds.SetActive(true);
        tempStageObject = null; //clearing temp object after use
        tempDiamondsObject = null; //clearing temp object after use
        player.transform.position = playerStartingPos;
        //reset score
    }

    public void LoadNextLevel()
    {

        LoadNextLevelStage();
        LoadNextLevelDiamonds();
    }
}

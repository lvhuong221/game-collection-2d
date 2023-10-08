using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToMoveGameManager : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;

    [Header("Events")]
    [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
    [SerializeField] private LoadSceneEventChannelSO unloadSceneEvent;
    [SerializeField] private VoidEventChannelSO playerHitGoalEvent;

    [Header("Variables")]
    [SerializeField] private GameObjectVariable playerObject;
    [SerializeField] private STMLevelSO levelSO;

    [Header("Level params")]
    [SerializeField] private IntVariable ammoLimit;

    STMLevelData currentLevel;

    private void Awake()
    {
        LoadLevelOnStart();
    }

    private void Start()
    {
        _inputReader.EnableShootToMoveInput();
    }

    private void OnEnable()
    {
        playerHitGoalEvent.OnEventRaised += OnPlayerHitGoal;
    }
    private void OnDisable()
    {
        playerHitGoalEvent.OnEventRaised -= OnPlayerHitGoal;
    }

    private void OnPlayerHitGoal()
    {
        //Debug.Log("Player hit goal");
        PlayerPrefs.SetInt(Constants.STM_Constants.STM_LEVEL_CLEARED, currentLevel.level);
        PlayerPrefs.Save();
        if (levelSO.IsLastLevel(currentLevel.level) == false)
        {
            LoadNextLevel();
        } else
        {
            LoadNextLevel();
        }
    }

    private void LoadLevelOnStart()
    {
        if (PlayerPrefs.HasKey(Constants.STM_Constants.STM_LEVEL_CLEARED) == false)
        {
            // Player have not cleared any level, we start from level 1
            currentLevel = levelSO.GetLevelData(1);

        } else
        {
            int level = PlayerPrefs.GetInt(Constants.STM_Constants.STM_LEVEL_CLEARED, 1) + 1;
            currentLevel = levelSO.GetLevelData(level);
        }

        if (currentLevel != null)
        {
            loadSceneEvent.Raise(currentLevel.sceneAsset, false, false);
        } else
        {
            Debug.LogWarning("Level data not found");
            // Start from level 1
            currentLevel = levelSO.GetLevelData(1);
            loadSceneEvent.Raise(levelSO.GetLevelData(1).sceneAsset, false, false);
        }
    }

    private void LoadNextLevel()
    {
        if (currentLevel == null)
        {
            return;
        }
        unloadSceneEvent.Raise(currentLevel.sceneAsset, false, false);

        int level = currentLevel.level + 1;

        currentLevel = levelSO.GetLevelData(level); 
        if (currentLevel != null)
        {
            loadSceneEvent.Raise(currentLevel.sceneAsset, false, false);
        } else
        {
            Debug.LogError("Level data not found");
            currentLevel = levelSO.GetLevelData(1);
            loadSceneEvent.Raise(levelSO.GetLevelData(1).sceneAsset, false, false);
        }
    }
}

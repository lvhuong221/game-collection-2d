using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations, Menus, Managers)
/// </summary>
public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReferenceScene sceneReference;
    //public AudioCueSO musicTrack;

    /// <summary>
    /// Used by the SceneSelector tool to discern what type of scene it needs to load
    /// </summary>
    public enum GameSceneType
    {
        // Playable scene
        Minigame,// SceneSelector tool will also load PersistentManagers and Gameplay
        Menu,// SceneSelector tool will also load Gameplay

        // Special scenes
        Initialisation,
        PersistentManagers,
        Gameplay,
    }
}

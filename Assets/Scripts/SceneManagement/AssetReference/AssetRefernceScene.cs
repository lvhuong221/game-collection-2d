using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AssetReferenceScene : AssetReferenceT<SceneAsset>
{
    public AssetReferenceScene(string guid) : base(guid)
    {
    }
}

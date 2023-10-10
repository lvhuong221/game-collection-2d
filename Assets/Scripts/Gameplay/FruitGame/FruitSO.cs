using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "FruitSO", menuName = "Gameplay Data/FruitSO")]
public class FruitSO : DescriptionBaseSO
{
    public Sprite icon;
    public GameObject prefab;
    public FruitSO combineResult;
    public int score;
}

